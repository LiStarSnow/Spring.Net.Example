using Cis.Infrastructure.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Core
{
    /// <summary>
    /// Excel操作类（NPOI）
    /// </summary>
    public static class ExcelHelper<T> where T : new()
    {
        /// <summary>
        /// 导入Excel到泛型集合
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<T> ImportExcelToList(string filePath)
        {
            List<T> list = new List<T>();
            IWorkbook workbook = null;
            bool is2007 = true;
            
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (filePath.IndexOf(".xlsx") > 0)// 2007版本
                    {
                        workbook = new XSSFWorkbook(file);
                        is2007 = true;
                    }
                    else if (filePath.IndexOf(".xls") > 0)// 2003版本
                    {
                        workbook = new HSSFWorkbook(file);
                        is2007 = false;
                    }
                    else
                    {
                        throw new Exception("Excel中没有数据！");
                    }
                    ISheet sheet = workbook.GetSheetAt(0);
                    if (sheet.PhysicalNumberOfRows == 0)
                    {
                        throw new Exception("Excel中没有数据！");
                    }
                    IRow headerRow = sheet.GetRow(0);//第一行为标题行
                    int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
                    int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1
                    Dictionary<int, string> dictColumnMapper = new Dictionary<int, string>();
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        dictColumnMapper.Add(i, headerRow.GetCell(i).StringCellValue);
                    }
                    for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        var mappedObj = new T();
                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    var propertyPath = dictColumnMapper[j];
                                    if(typeof(T).GetProperty(propertyPath).PropertyType == typeof(DateTime?))
                                    {
                                        string dataStr = string.Empty;
                                        try
                                        {
                                            dataStr = row.GetCell(j).DateCellValue.ToString();
                                        }
                                        catch
                                        {
                                            dataStr = row.GetCell(j).StringCellValue;
                                            dataStr = string.IsNullOrWhiteSpace(dataStr) ? DateTime.Now.ToString() : dataStr;
                                        }
                                        Reflection.SetPropertyValueByPath(mappedObj, DateTime.Parse(dataStr), propertyPath);
                                    }
                                    else
                                    {
                                        string value = GetCellValue(row.GetCell(j), is2007);
                                        Reflection.SetPropertyValueByPath(mappedObj, value, propertyPath);
                                    }
                                    
                                }
                            }
                        }
                        list.Add(mappedObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        /// <summary>
        /// 根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell, bool flag)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        IFormulaEvaluator e = null;
                        if (flag)
                        {
                            e= new XSSFFormulaEvaluator(cell.Sheet.Workbook);
                        }
                        else
                        {
                            e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        }                        
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
    }
}
