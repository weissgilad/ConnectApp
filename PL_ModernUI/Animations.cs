using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SciChart.Wpf.UI.Transitionz;
using System.Windows;

namespace PL_ModernUI
{
    /// <summary>
    /// static fade in/out animations
    /// </summary>
    internal class Animations
    {
        /// <summary>
        /// to 5 so it will stay at %100 for some time
        /// </summary>
        private static OpacityParams opacity = new OpacityParams()
        {
            AutoReverse = true,
            From = 0,
            To = 5,
            Duration = 1300
        };

        private static OpacityParams opacity_off = new OpacityParams()
        {
            From = 1,
            To = 0,
            Duration = 350
        };

        private static OpacityParams opacity_on = new OpacityParams()
        {
            From = 0,
            To = 1,
            Duration = 350
        };

        /// <summary>
        /// fade in, stay, then fade out
        /// </summary>
        /// <param name="element">element to animate</param>
        internal static void OpacityAnimation(UIElement element)
        {
            Transitionz.SetOpacity(element, new OpacityParams());
            Transitionz.SetOpacity(element, opacity);
        }

        /// <summary>
        /// fade out element over 350ms
        /// </summary>
        /// <param name="element">element to animate</param>
        internal static void FadeOut(UIElement element)
        {
            Transitionz.SetOpacity(element, opacity_off);
        }
        /// <summary>
        /// fade in element over 350ms
        /// </summary>
        /// <param name="element">element to animate</param>
        internal static void FadeIn(UIElement element)
        {
            Transitionz.SetOpacity(element, opacity_on);
        }
    }
}
