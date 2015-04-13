﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MODEL;
namespace DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- Users的实体类.
    /// 创建时间:2015/1/28 20:50:41
    /// </summary>
    public class Users
    {
        #region 01.修改软删除标志
        /// <summary>
        /// 修改软删除标志
        /// </summary>
        /// <param name="ids">要修改软删除标志的id号们(1,2,5)</param>
        /// <param name="isDel">要将删除标志 改成 true/false</param>
        /// <returns>受影响行数</returns>
        public int UpdateDel(string ids, bool isDel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec('update Users set uIsDel=''" + isDel.ToString() + "'' where uId in ('+@ids+')')");
            SqlParameter para = new SqlParameter("@ids", ids);
            return DbHelperSQL.ExcuteNonQuery(strSql.ToString(), para);
        }
        #endregion

        #region 01.2单个修改软删除标志
        /// <summary>
        /// 单个修改软删除标志
        /// </summary>
        /// <param name="idInt">要修改软删除标志的id号</param>
        /// <param name="isDel">要将删除标志 改成 true/false</param>
        /// <returns>受影响行数</returns>
        public int UpdateDel(int idInt, bool isDel)
        {
            string strSql = "update Users set uIsDel='" + isDel.ToString() + "' where uId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 02.执行物理删除 +int Del(string ids)
        /// <summary>
        /// 执行物理删除
        /// </summary>
        /// <param name="ids">要删除的id号们(1,2,5)</param>
        /// <returns>受影响行数</returns>
        public int Del(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec('delete Users where uId in ('+@ids+')')");
            SqlParameter para = new SqlParameter("@ids", ids);
            return DbHelperSQL.ExcuteNonQuery(strSql.ToString(), para);
        }
        #endregion

        #region 02.2单个物理删除
        /// <summary>
        /// 单个物理删除
        /// </summary>
        /// <param name="idInt">要删除的id号</param>
        /// <returns>受影响行数</returns>
        public int Del(int idInt)
        {
            string strSql = "delete Users where uId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.Users GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public MODEL.Users GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select uId,uName,uLoginName,uPwd,uAddtime,uIsDel from Users ");
            strSql.Append(" where uId=@intId ");
            MODEL.Users model = new MODEL.Users();
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString(), new SqlParameter("@intId", intId));
            if (dt.Rows.Count > 0)
            {
                LoadEntityData(model, dt.Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 04.查询数据集合 +IList<MODEL.Users> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<MODEL.Users> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<MODEL.Users> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<MODEL.Users> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select uId,uName,uLoginName,uPwd,uAddtime,uIsDel ");
            strSql.Append(" FROM Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<MODEL.Users> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.Users> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<MODEL.Users> Table2List(DataTable dt)
        {
            List<MODEL.Users> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<MODEL.Users>();
                MODEL.Users model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new MODEL.Users();
                    LoadEntityData(model, dr);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }
        #endregion

        #region a04.加载实体数据(将行数据装入实体对象中)-void LoadEntityData(MODEL.BlogArticleCate model, DataRow dr)
        /// <summary>
        /// 加载实体数据(将行数据装入实体对象中)
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dr">数据行</param>
        internal void LoadEntityData(MODEL.Users model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("uId") && !dr.IsNull("uId"))
            {
                model.UId = int.Parse(dr["uId"].ToString());
            }
            model.UName = dr["uName"].ToString();
            model.ULoginName = dr["uLoginName"].ToString();
            model.UPwd = dr["uPwd"].ToString();
            if (dr.Table.Columns.Contains("uAddtime") && !dr.IsNull("uAddtime"))
            {
                model.UAddtime = DateTime.Parse(dr["uAddtime"].ToString());
            }
            if (dr.Table.Columns.Contains("uIsDel") && !dr.IsNull("uIsDel"))
            {
                model.UIsDel = bool.Parse(dr["uIsDel"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(MODEL.Users model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Users(");
                strSql.Append("uName,uLoginName,uPwd,uAddtime,uIsDel)");
                strSql.Append(" values (");
                strSql.Append("@uName,@uLoginName,@uPwd,@uAddtime,@uIsDel)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@uName", SqlDbType.VarChar,20),
                    new SqlParameter("@uLoginName", SqlDbType.VarChar,20),
                    new SqlParameter("@uPwd", SqlDbType.VarChar,32),
                    new SqlParameter("@uAddtime", SqlDbType.DateTime,8),
                    new SqlParameter("@uIsDel", SqlDbType.Bit,1)};
                parameters[0].Value = model.UName;
                parameters[1].Value = model.ULoginName;
                parameters[2].Value = model.UPwd;
                parameters[3].Value = model.UAddtime;
                parameters[4].Value = model.UIsDel;
                result = Convert.ToInt32(DbHelperSQL.ExcuteScalar(strSql.ToString(), parameters));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region 08.修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>修改成功的行数</returns>
        public int Update(MODEL.Users model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users set ");
            strSql.Append("uName=@uName,");
            strSql.Append("uLoginName=@uLoginName,");
            strSql.Append("uPwd=@uPwd,");
            strSql.Append("uAddtime=@uAddtime,");
            strSql.Append("uIsDel=@uIsDel");
            strSql.Append(" where uId=@uId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@uId", SqlDbType.Int,4),
                    new SqlParameter("@uName", SqlDbType.VarChar,20),
                    new SqlParameter("@uLoginName", SqlDbType.VarChar,20),
                    new SqlParameter("@uPwd", SqlDbType.VarChar,32),
                    new SqlParameter("@uAddtime", SqlDbType.DateTime,8),
                    new SqlParameter("@uIsDel", SqlDbType.Bit,1)};
            parameters[0].Value = model.UId;
            parameters[1].Value = model.UName;
            parameters[2].Value = model.ULoginName;
            parameters[3].Value = model.UPwd;
            parameters[4].Value = model.UAddtime;
            parameters[5].Value = model.UIsDel;

            try
            {
                res = DbHelperSQL.ExcuteNonQuery(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        #endregion

        public MODEL.Users FindUser(MODEL.Users u)
        {
            StringBuilder strSql = new StringBuilder("select uId,uName,uLoginName,uPwd,uAddtime,uIsDel from Users ");
            strSql.Append(" where uLoginName=@uLoginName ");
            MODEL.Users model = new MODEL.Users();
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString(), new SqlParameter("@uLoginName", u.ULoginName));
            if (dt.Rows.Count > 0)
            {
                LoadEntityData(model, dt.Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
