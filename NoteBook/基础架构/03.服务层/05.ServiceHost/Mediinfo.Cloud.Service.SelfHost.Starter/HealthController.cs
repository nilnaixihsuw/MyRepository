using System.Web.Http;

namespace Mediinfo.Cloud.Service.SelfHost.Starter
{
    /// <summary>
    /// 健康检查
    /// </summary>
    public class HealthController : ApiController
    {
        /// <summary>
        /// 健康检查接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Health")]
        public string Index()
        {
            //OracleConnection conn = null;
            //try
            //{
            //    conn = new OracleConnection(MediinfoConfig.GetValue("DbConfig.xml", "HIS6"));
            //    conn.Open();
            //    OracleCommand cmd = new OracleCommand("SELECT 'DBOK' from dual", conn);
            //    cmd.CommandType = System.Data.CommandType.Text;
            //    var result = cmd.ExecuteScalar().ToString();
            //    return result;

            //}
            //catch (Exception ex)
            //{
            //    Mediinfo.Enterprise.Log.LogHelper.Intance.Error("健康检查", "健康检查发生错误！", ex.ToString());
            //}
            //finally
            //{
            //    if (conn != null && conn.State != System.Data.ConnectionState.Closed)
            //        conn.Close();
            //}

            return "OK";
        }
    }
}
