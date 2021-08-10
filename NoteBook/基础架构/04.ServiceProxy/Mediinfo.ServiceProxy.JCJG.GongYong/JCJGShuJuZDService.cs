//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.ServiceProxy.JCJG.GongYong
{
    public partial class JCJGShuJuZDService
    {
        public ServiceClient serviceClient = null;
        public JCJGShuJuZDService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Data.DataSet> GetShuJuZDList(System.Collections.Generic.List<System.String> shuJuZDIdList)
        {
            return serviceClient.Invoke<System.Data.DataSet>("JCJGShuJuZD", "GetShuJuZDList",new ServiceParm(nameof(shuJuZDIdList), shuJuZDIdList));
        }
        public async Task<Result<System.Data.DataSet>> GetShuJuZDListAsync(System.Collections.Generic.List<System.String> shuJuZDIdList)
        {
            return await serviceClient.InvokeAsync<System.Data.DataSet>("JCJGShuJuZD", "GetShuJuZDList",new ServiceParm(nameof(shuJuZDIdList), shuJuZDIdList));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>> GetAllShuJuZD()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>>("JCJGShuJuZD", "GetAllShuJuZD");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>>> GetAllShuJuZDAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>>("JCJGShuJuZD", "GetAllShuJuZD");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>> GetShuJuZDByYingYong(System.String yingYongId)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>>("JCJGShuJuZD", "GetShuJuZDByYingYong",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>>> GetShuJuZDByYingYongAsync(System.String yingYongId)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL3>>("JCJGShuJuZD", "GetShuJuZDByYingYong",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public Result<System.Boolean> SaveShuJuZDHC(System.String yingYongID,System.Collections.Generic.List<System.String> sqlIDList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGShuJuZD", "SaveShuJuZDHC",new ServiceParm(nameof(yingYongID), yingYongID),new ServiceParm(nameof(sqlIDList), sqlIDList));
        }
        public async Task<Result<System.Boolean>> SaveShuJuZDHCAsync(System.String yingYongID,System.Collections.Generic.List<System.String> sqlIDList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGShuJuZD", "SaveShuJuZDHC",new ServiceParm(nameof(yingYongID), yingYongID),new ServiceParm(nameof(sqlIDList), sqlIDList));
        }
        public Result<Mediinfo.Enterprise.JsonDataSet> GetByYingYongId(System.String yingYongId)
        {
            return serviceClient.Invoke<Mediinfo.Enterprise.JsonDataSet>("JCJGShuJuZD", "GetByYingYongId",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public async Task<Result<Mediinfo.Enterprise.JsonDataSet>> GetByYingYongIdAsync(System.String yingYongId)
        {
            return await serviceClient.InvokeAsync<Mediinfo.Enterprise.JsonDataSet>("JCJGShuJuZD", "GetByYingYongId",new ServiceParm(nameof(yingYongId), yingYongId));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

