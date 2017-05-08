
namespace Cis.Model.Dto.Config
{
    public class LangResult
    {
        /// <summary>
        /// 资源字段Key
        /// </summary>
        public string FieldId { get; set; }

        /// <summary>
        /// 资源字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 资源字段重命名
        /// </summary>
        public string FieldReName { get; set; }

        /// <summary>
        /// 资源字段备注
        /// </summary>
        public string FieldRemark { get; set; }


        public int NumRow { get; set; }

        /// <summary>
        /// 字段类型 (1:普通类型;2:扩展类型)
        /// </summary>
        public string FieldType { get; set; }

    }
}
