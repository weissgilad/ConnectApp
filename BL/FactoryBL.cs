using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// class to implement sigleton method
    /// </summary>
    sealed public class FactoryBL
    {
        private static readonly IBL bl = new BL_imp();

        /// <summary>
        /// access to implementation
        /// </summary>
        /// <returns>BL implementation</returns>
        public static IBL GetBL()
        {
            return bl;
        }

    }
}
