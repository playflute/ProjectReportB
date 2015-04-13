using System;
namespace MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- Users表映射类.
    /// 最后修改时间:2015/1/28 20:54:42
    /// </summary>
    public class Users
    {
        public Users()
        { }

        #region 字段们
        protected int _uId;
        protected string _uName;
        protected string _uLoginName;
        protected string _uPwd;
        protected DateTime _uAddtime;
        protected bool _uIsDel;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int UId
        {
            set { _uId = value; }
            get { return _uId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UName
        {
            set { _uName = value; }
            get { return _uName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ULoginName
        {
            set { _uLoginName = value; }
            get { return _uLoginName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UPwd
        {
            set { _uPwd = value; }
            get { return _uPwd; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UAddtime
        {
            set { _uAddtime = value; }
            get { return _uAddtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UIsDel
        {
            set { _uIsDel = value; }
            get { return _uIsDel; }
        }
        #endregion
    }
}