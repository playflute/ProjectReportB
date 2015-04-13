using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class TripletsSet
    {
        public TripletsSet()
        { }

        #region 字段们
        protected int _id;
        protected DateTime _timeStamp;
        protected string _subject;
        protected string _verb;
        protected string _object;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeStamp
        {
            set { _timeStamp = value; }
            get { return _timeStamp; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Verb
        {
            set { _verb = value; }
            get { return _verb; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Object
        {
            set { _object = value; }
            get { return _object; }
        }
        #endregion
    }
}
