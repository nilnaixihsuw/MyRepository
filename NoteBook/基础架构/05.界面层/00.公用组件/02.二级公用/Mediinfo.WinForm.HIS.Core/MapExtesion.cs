using Mediinfo.DTO.Core;
using NLite;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// DTO转换类
    /// </summary>
    public static class MapExtesion
    {
        /// <summary>
        /// 复制DTO
        /// </summary>
        /// <typeparam name="W">DTOBase</typeparam>
        /// <typeparam name="T">DTOBase</typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static List<T> EToE<W, T>(this List<W> entitys)
         where W : DTOBase
         where T : DTOBase
        {
            return MapList<W, T>(entitys);
        }

        /// <summary>
        /// 复制dto
        /// </summary>
        /// <typeparam name="W">DTOBase</typeparam>
        /// <typeparam name="T">DTOBase</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T EToE<W, T>(this W entity)
         where W : DTOBase
         where T : DTOBase
        {

            return MapEntity<W, T>(entity);
        }

        /// <summary>
        /// 合并list
        /// </summary>
        /// <typeparam name="W">class</typeparam>
        /// <typeparam name="T">class</typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static List<T> MapList<W, T>(List<W> entitys)
            where T : class
            where W : class
        {

            return entitys.Select(s => MapEntity<W, T>(s)).ToList();
            //return Mapper.Map<List<W>, List<T>>(dbEntity);
        }

        /// <summary>
        /// 合并类型
        /// </summary>
        /// <typeparam name="W">class</typeparam>
        /// <typeparam name="T">class</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T MapEntity<W, T>(W entity)
            where T : class
            where W : class
        {
            return Mapper.Map<W, T>(entity);
        }
    }
}
