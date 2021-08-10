using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_FEIYONGKZRepository : RepositoryBase<GY_FEIYONGKZ>, IGY_FEIYONGKZRepository
	{
		public GY_FEIYONGKZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 获取费用控制（含已作废）
        /// </summary>
        /// <param name="feiYongXZ">费用性质Id</param>
        /// <returns></returns>
        public GY_FEIYONGKZ Get(string feiYongXZ)
        {
            return (from a in this.Set<GY_FEIYONGKZ>()
                    join b in this.Set<GY_FEIYONGXZ>()
                    on a.FEIYONGKZID equals b.FEIYONGKZID 
                    where b.XINGZHIID == feiYongXZ
                    select a).FirstOrDefault().WithContext(this,this.ServiceContext);
        }

        /// <summary>
        /// 获取费用控制（含已作废）
        /// </summary>
        /// <param name="yingYongId">应用Id</param>
        /// <param name="feiYongXZ">费用性质Id</param>
        /// <returns></returns>
        public GY_FEIYONGKZ Get(string yingYongId, string feiYongXZ)
        {
            var feiYongKZ = (from a in this.Set<GY_FEIYONGKZ>()
                            join b in this.Set<GY_FEIYONGKZDY>()
                            on a.FEIYONGKZID equals b.FEIYONGKZID
                            where b.FEIYONGXZ == feiYongXZ
                                && b.YINGYONGID == yingYongId
                                && b.ZUOFEIBZ != 1  //费用控制对应中的作废标志需要判断
                            select a).FirstOrDefault().WithContext(this,this.ServiceContext);

            //如果根据yingyongID和feiYongXZ找不到，就根据feiYongXZ去找
            if (null == feiYongKZ)
                return this.Get(feiYongXZ);
            else
                return feiYongKZ;
        }
        /// <summary>
        /// his1调用存储过程获取调价金额
        /// </summary>
        /// <param name="xiangMuXX_XML"></param>
        /// <returns></returns>
        public decimal GetHis1DanJia(string xiangMuXX_XML,ref int jiaGeTX)
        { 
            decimal newDanJia = 0;  
            OracleParameter[] paramJiaoYi ={
                                new OracleParameter("as_in_xiangmuxx_xml",OracleDbType.Varchar2,ParameterDirection.Input),
                                 new OracleParameter("ai_debugltms",OracleDbType.Decimal,ParameterDirection.Input),
                                new OracleParameter("as_out_xiangmuxx_xml",OracleDbType.Varchar2,500,"",ParameterDirection.Output),
                                new OracleParameter("ad_newdj",OracleDbType.Decimal,10,"",ParameterDirection.Output),
                                new OracleParameter("ai_jiagelb",OracleDbType.Int32,10,"",ParameterDirection.Output),
                                new OracleParameter("prm_appcode",OracleDbType.Decimal,10,"",ParameterDirection.Output),
                                new OracleParameter("as_errmsg",OracleDbType.Varchar2,100,"",ParameterDirection.Output)
                                                };
            paramJiaoYi[0].Value = xiangMuXX_XML;
           
            this.ExecuteProc($"Pkg_His_JiaGeTxYw.Prc_His_JiaGeTx", paramJiaoYi);

            newDanJia = paramJiaoYi[3].Value.ToStringEx().ToDecimal();
            var jiaGeLB = paramJiaoYi[4].Value.ToStringEx().ToInt();

            jiaGeTX = (jiaGeLB == 0) ? jiaGeTX : jiaGeLB;//his1中普通的为0，转为1

            return newDanJia;
        }
    }
}
