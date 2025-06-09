using System.Drawing;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    internal static class ScreenLayout
    {
        // NOTE: Forms/Controls can be set to these values in ctors, but the Width/Height need to 
        // be manually set in the designers to aid visual designing as they don't work well with constants.

        public const int SECTION_WIDTH = 370;
        public const int SECTION_HEIGHT = 65;

        public static Size SectionSize { get; } = new(SECTION_WIDTH, SECTION_HEIGHT);

        public static void SetToSectionSizeFixed(BuildDetailControl control)
        {
            control.Size = SectionSize;
            control.MaximumSize = SectionSize;
            control.MinimumSize = SectionSize;
        }

        public static void SetToSectionSizeWithoutMaximum(Control control)
        {
            control.Size = SectionSize;
            control.MinimumSize = SectionSize;
        }

        public static void SetToSectionSizeWithZeroMinHeight(Control control)
        {
            control.Size = SectionSize;
            control.MaximumSize = SectionSize;
            control.MinimumSize = new(SECTION_WIDTH, 0);
        }

        public const int OFFSET_X = 10;
        public const int OFFSET_Y = 3;
    }
}
