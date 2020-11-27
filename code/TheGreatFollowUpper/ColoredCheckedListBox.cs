using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    public partial class ColoredCheckedListBox : CheckedListBox
    {
        public ColoredCheckedListBox()
        {
            InitializeComponent();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.DesignMode)
                base.OnDrawItem(e);
            else
            {
                var masterCategoryName = this.Items[e.Index].ToString();
                var masterCategory = Globals.GreatFollowUpperAddin.Application.Session.Categories.Cast<Outlook.Category>().SingleOrDefault(c => c.Name == masterCategoryName);

                if (masterCategory == null)
                    base.OnDrawItem(e);
                else
                {
                    var color = ColorTranslator.FromOle((int) masterCategory.CategoryGradientBottomColor);

                    var e2 = new DrawItemEventArgs(
                        e.Graphics, 
                        e.Font, 
                        new Rectangle(e.Bounds.Location, e.Bounds.Size), 
                        e.Index, 
                        (e.State & DrawItemState.Focus) == DrawItemState.Focus ? DrawItemState.Focus : DrawItemState.None, /* Remove 'selected' state so that the base.OnDrawItem doesn't obliterate the work we are doing here. */
                        PerceivedBrightness(color) > 130 ? Color.Black : Color.White, 
                        color);

                    base.OnDrawItem(e2);
                }
            }
        }

        private static int PerceivedBrightness(Color c)
        {
            // https://stackoverflow.com/questions/2241447/make-foregroundcolor-black-or-white-depending-on-background

            return (int)Math.Sqrt(
                c.R * c.R * .299 +
                c.G * c.G * .587 +
                c.B * c.B * .114);
        }
    }
}
