using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  Users表的业务操作类.
    /// 时间:2015/1/28 20:53:40
    /// </summary>
    public class Users
    {
        private readonly DAL.Users dal = new DAL.Users();

        #region 01.根据ID获得实体对象 +MODEL.Users GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public MODEL.Users GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<MODEL.Users> GetList()
        {
            return dal.GetList();
        }
        #endregion

        #region 03.还原 +int UpdateRe(string ids)
        /// <summary>
        /// 还原（将删除标志设置为false）
        /// </summary>
        /// <param name="ids">要还原的 id们</param>
        /// <returns>还原成功与否</returns>
        public bool UpdateRe(string ids)
        {
            return dal.UpdateDel(ids, false) > 0;
        }
        #endregion

        #region 04.软删除+ int UpdateDel(string ids)
        /// <summary>
        /// 软删除（将删除标志设置为true）
        /// </summary>
        /// <param name="ids">要软删除的 id们</param>
        /// <returns>软删除成功与否</returns>
        public bool UpdateDel(string ids)
        {
            return dal.UpdateDel(ids, true) > 0;
        }
        #endregion

        #region 05.物理删除 +int Del(string ids)
        /// <summary>
        /// 物理删除（将删除标志设置为true）
        /// </summary>
        /// <param name="ids">要删除的 id们</param>
        /// <returns>删除成功与否</returns>
        public bool Del(string ids)
        {
            return dal.Del(ids) > 0;
        }
        #endregion

        #region 06.新增记录
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增行的ID</returns>
        public int Add(MODEL.Users model)
        {
            return dal.Add(model);
        }
        #endregion

        #region 07.修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>受影响行数</returns>
        public bool Update(MODEL.Users model)
        {
            return dal.Update(model) > 0;
        }
        #endregion
        public MODEL.Users FindUser(MODEL.Users u)
        {
            return dal.FindUser(u);
        }
    }
}