using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class TripletsSet
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
            strSql.Append("exec('update TripletsSet set noDelKey=''" + isDel.ToString() + "'' where Id in ('+@ids+')')");
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
            string strSql = "update TripletsSet set noDelKey='" + isDel.ToString() + "' where Id = @id";
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
            strSql.Append("exec('delete TripletsSet where Id in ('+@ids+')')");
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
            string strSql = "delete TripletsSet where Id = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.TripletsSet GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public MODEL.TripletsSet GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select Id,TimeStamp,Subject,Verb,Object from TripletsSet ");
            strSql.Append(" where Id=@intId ");
            MODEL.TripletsSet model = new MODEL.TripletsSet();
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

        #region 04.查询数据集合 +IList<MODEL.TripletsSet> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<MODEL.TripletsSet> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<MODEL.TripletsSet> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<MODEL.TripletsSet> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TimeStamp,Subject,Verb,Object ");
            strSql.Append(" FROM TripletsSet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<MODEL.TripletsSet> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.TripletsSet> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<MODEL.TripletsSet> Table2List(DataTable dt)
        {
            List<MODEL.TripletsSet> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<MODEL.TripletsSet>();
                MODEL.TripletsSet model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new MODEL.TripletsSet();
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
        internal void LoadEntityData(MODEL.TripletsSet model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("Id") && !dr.IsNull("Id"))
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr.Table.Columns.Contains("TimeStamp") && !dr.IsNull("TimeStamp"))
            {
                model.TimeStamp = DateTime.Parse(dr["TimeStamp"].ToString());
            }
            model.Subject = dr["Subject"].ToString();
            model.Verb = dr["Verb"].ToString();
            model.Object = dr["Object"].ToString();

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(MODEL.TripletsSet model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TripletsSet(");
                strSql.Append("TimeStamp,Subject,Verb,Object)");
                strSql.Append(" values (");
                strSql.Append("@TimeStamp,@Subject,@Verb,@Object)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@TimeStamp", SqlDbType.DateTime,8),
                    new SqlParameter("@Subject", SqlDbType.VarChar,-1),
                    new SqlParameter("@Verb", SqlDbType.VarChar,-1),
                    new SqlParameter("@Object", SqlDbType.VarChar,-1)};
                parameters[0].Value = model.TimeStamp;
                parameters[1].Value = model.Subject;
                parameters[2].Value = model.Verb;
                parameters[3].Value = model.Object;
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
        public int Update(MODEL.TripletsSet model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TripletsSet set ");
            strSql.Append("TimeStamp=@TimeStamp,");
            strSql.Append("Subject=@Subject,");
            strSql.Append("Verb=@Verb,");
            strSql.Append("Object=@Object");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@TimeStamp", SqlDbType.DateTime,8),
                    new SqlParameter("@Subject", SqlDbType.VarChar,-1),
                    new SqlParameter("@Verb", SqlDbType.VarChar,-1),
                    new SqlParameter("@Object", SqlDbType.VarChar,-1)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TimeStamp;
            parameters[2].Value = model.Subject;
            parameters[3].Value = model.Verb;
            parameters[4].Value = model.Object;

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

        public MODEL.PageInfo getPageList(int PageNo, int PageSize)
        {
            MODEL.PageInfo pi = new MODEL.PageInfo();
            //int total_row_number;
            //int total_page_number;
            pi.list= Table2List( DbHelperSQL.GetPageListByProc("usp_Pagination",PageNo,PageSize,out pi.total_row_number,out pi.total_page_number));
            return pi;
        }
    }


}
