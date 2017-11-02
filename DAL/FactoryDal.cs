using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// class to implement sigleton method
    /// </summary>
    sealed public class FactoryDal
    {
        private static readonly Idal dal = new Dal_XML_imp();
        //private static readonly Idal dal = new Dal_imp();

        /// <summary>
        /// access to implementation
        /// </summary>
        /// <returns>DAL implementation</returns>
        public static Idal GetDal()
        {
            return dal;
        }


    }
}
