using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Oatc.OpenMI.Sdk.Backbone;
using Oatc.OpenMI.Sdk.DevelopmentSupport;
using OpenMI.Standard;
using TimeSpan = Oatc.OpenMI.Sdk.Backbone.TimeSpan;

namespace OOC.OpenMIComponent.DataMonitor
{
    public abstract class DataMonitor : LinkableComponent, IListener
    {
        protected String id = "OOC Data Monitor";

        protected abstract void DataArrived(DateTime simulationTime, string modelId, string quantity, string elementSet, IScalarSet scalarSet);

        public override void Prepare()
        {
        }

        public override string ComponentDescription
        {
            get
            {
                return "Data Monitor";
            }
        }

        public override string ComponentID
        {
            get
            {
                return id;
            }
        }

        public override string ModelID
        {
            get
            {
                return id;
            }
        }

        public override string ModelDescription
        {
            get
            {
                return "Data Monitor";
            }
        }

        public override void Initialize(IArgument[] properties)
        {
            foreach (IArgument argument in properties)
            {
                if (argument.Key == "ID")
                {
                    id = argument.Value;
                }
            }
        }

        public override ITimeStamp EarliestInputTime
        {
            get
            {
                return new TimeStamp(0.0);
            }
        }

        public override ITimeSpan TimeHorizon
        {
            get
            {
                return new TimeSpan(new TimeStamp(0.0), new TimeStamp(1e6));
            }
        }

        public override EventType GetPublishedEventType(int providedEventTypeIndex)
        {
            return new EventType();
        }

        public override int GetPublishedEventTypeCount()
        {
            return 0;
        }

        public override IValueSet GetValues(ITime time, string LinkID)
        {
            return null;
        }

        public override string Validate()
        {
            return "";
        }

        public override void Finish()
        {
        }
        
        private ArrayList _acceptedEventTypes = new ArrayList();

        public DataMonitor()
        {
            _acceptedEventTypes.Add(EventType.DataChanged);
        }

        public override void AddLink(ILink link)
        {
            ILinkableComponent LC = link.SourceComponent;
            for (int i = 0; i < GetAcceptedEventTypeCount(); i++)
            {
                EventType ev = GetAcceptedEventType(i);
                LC.Subscribe(this, ev);
            }
            base.AddLink(link);
        }

        public void OnEvent(IEvent Event)
        {
            ILink[] links = GetAcceptingLinks();
            foreach (ILink link in links)
            {
                if (link.SourceComponent == Event.Sender)
                {
                    IValueSet values = Event.Sender.GetValues(Event.SimulationTime, link.ID);

                    if (values is IScalarSet)
                    {
                        IScalarSet scalarSet = (IScalarSet)values;
                        DataArrived(CalendarConverter.ModifiedJulian2Gregorian(Event.SimulationTime.ModifiedJulianDay), 
                            Event.Sender.ModelID, link.SourceQuantity.ID, link.SourceElementSet.ID, scalarSet);
                    }
                }
            }
        }

        public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
        {
            return (EventType)_acceptedEventTypes[acceptedEventTypeIndex];
        }

        public int GetAcceptedEventTypeCount()
        {
            return _acceptedEventTypes.Count;
        }

        public override IInputExchangeItem GetInputExchangeItem(int inputExchangeItemIndex)
        {
            Quantity quantity = new Quantity(new Unit("Unit", 1.0, 0.0, "Unit"), "Quantity", "Quantity");
            ElementSet elementSet = new ElementSet("1", "1", ElementType.IDBased,
                new SpatialReference());
            InputExchangeItem exchangeItem = new InputExchangeItem();
            exchangeItem.ElementSet = elementSet;
            exchangeItem.Quantity = quantity;
            return exchangeItem;
        }

        public override int InputExchangeItemCount
        {
            get
            {
                return 1;
            }
        }
    }

}
