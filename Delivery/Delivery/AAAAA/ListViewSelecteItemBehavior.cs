using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BehaviorsDemo
{
    public class ListViewSelecteItemBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create(
                "ItemTappedCommand",
                typeof(ICommand),
                typeof(ListViewSelecteItemBehavior),
                null);


        public ICommand ItemTappedCommand
        {
            get
            {
                return (ICommand)GetValue(ItemTappedCommandProperty);
            }
            set
            {
                SetValue(ItemTappedCommandProperty, value);
            }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            //bindable.ItemSelected += OnListViewItemSelected;
            bindable.ItemTapped += OnListViewItemSelected;
        }
        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            //bindable.ItemSelected -= OnListViewItemSelected;
            bindable.ItemTapped -= OnListViewItemSelected;
        }

        //void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        void OnListViewItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (ItemTappedCommand == null)
            {
                return;
            }

            if (ItemTappedCommand.CanExecute(e.Item))
            {
                ItemTappedCommand.Execute(e.Item);
            }
        }
    }
}
