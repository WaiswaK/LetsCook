using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCook.Component
{
    class EventToCommandBehavior : Behavior<VisualElement>
    {
        private EventInfo _ei;
        private Delegate _handler;

        // EventName property
        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create("EventName", typeof(string),
                typeof(EventToCommandBehavior), String.Empty);

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        // Command property
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand),
            typeof(EventToCommandBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(VisualElement view)
        {
            if (!String.IsNullOrEmpty(EventName))
            {
                _ei = view.GetType().GetRuntimeEvent(EventName);

                if (_ei == null)
                    throw new ArgumentException(String.Format("Event {0} not found", EventName), "EventName");

                MethodInfo mi = GetType().GetTypeInfo().GetDeclaredMethod("InternalEventHandler");
                _handler = mi.CreateDelegate(_ei.EventHandlerType, this);
                _ei.AddEventHandler(view, _handler);
            }
        }

        private void InternalEventHandler(object sender, EventArgs args)
        {
            if (Command != null && Command.CanExecute(args))
                Command.Execute(args);
        }

        protected override void OnDetachingFrom(VisualElement view)
        {
            if (_handler != null)
                _ei.RemoveEventHandler(view, _handler);
        }
    }
}
