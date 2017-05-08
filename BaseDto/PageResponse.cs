using System.ComponentModel;
using System;

namespace BaseDto
{
    /// <summary>
    /// 分页列表响应
    /// </summary>
    [Description("分页列表响应")]
    public class PageResponse<T> : Response<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        [Description("总数")]
        public int? TotalCount { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        [Description("分页大小")]
        public int PageSize { get; set; }

        private int _pageNum = 1;
        /// <summary>
        /// 页索引
        /// </summary>
        [Description("页索引")]
        public int PageNum
        {
            get
            {
                if (_pageNum < 1)
                {
                    _pageNum = 1;
                }
                if (TotalCount > 0 && _pageNum > Math.Ceiling((double)TotalCount / PageSize))
                {
                    _pageNum = (int)Math.Ceiling((double)TotalCount / PageSize);
                }
                return _pageNum;
            }
            set
            {
                _pageNum = value;
            }
        }
    }
}
