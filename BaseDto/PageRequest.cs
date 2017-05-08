using System.ComponentModel;

namespace BaseDto
{
    /// <summary>
    /// 请求
    /// </summary>
    [Description("请求")]
    public abstract class PageRequst
    {
        private int _pageNum = 1;
        public int PageNum
        {
            get
            {
                if (_pageNum < 1)
                {
                    _pageNum = 1;
                }
                return _pageNum;
            }
            set
            {
                _pageNum = value;
            }
        }

        public int PageSize { get; set; } = 10;

        public int? TotalCount { get; set; }

        public int BeginIndex
        {
            get { return (_pageNum - 1) * PageSize; }
        }

        public int EndIndex
        {
            get { return _pageNum * PageSize; }
        }
    }
}
