using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_JIARIPAIBANRepository : IRepository<GY_JIARIPAIBAN>, IDependency
    {
        int GetCount(string jiuZhenKS, int zuoZhenLX, string jiuZhenYS);

        int GetCount(string jiuZhenKS, int zuoZhenLX);

        int GetCount(int zuoZhenLX);


        void GetString(string jiuZhenKS, string jiuZhenYS, int zuoZhenLX, out string S_JIEJIARPBID,
             out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB);

        void GetString(string jiuZhenKS, int zuoZhenLX, out string S_JIEJIARPBID,
            out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB);

        void GetString(int zuoZhenLX, out string S_JIEJIARPBID,
            out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB);
    }
}
