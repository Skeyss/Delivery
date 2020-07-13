using Delivery.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.Behavior
{
    public class SetFocusOnEntryCompletedBehavior: Behavior<MyEntry>
    {
        public string NextFocusElementName { get; set; }

        protected override void OnAttachedTo(MyEntry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += Bindable_Completed;
        }

        protected override void OnDetachingFrom(MyEntry bindable)
        {
            bindable.Completed -= Bindable_Completed;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NextFocusElementName))
                return;

            var parent = ((MyEntry)sender).Parent;
            while (parent != null)
            {
                var nextFocusElement = parent.FindByName<MyEntry>(NextFocusElementName);
                if (nextFocusElement != null)
                {
                    nextFocusElement.Focus();
                    break;
                }
                else
                {
                    parent = parent.Parent;
                }
            }
        }
    }
}
