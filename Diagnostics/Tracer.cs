namespace Ideal.Diagnostics
{
    public interface Tracer
    {
        /**
	 * Close the tracer
	 * @return true if tracer ok
	 */
        public bool Close();

        /**
         * Create a new event
         * @param t the time stamp
         */
        public void StartNewEvent(int t);

        /**
         * Closes the current event.
         */
        public void FinishEvent();

        /**
         * Add a new property to the current event
         * @param name The property's name
         * @return the added event element
         */
        public EventElement AddEventElement(string name);

        /**
         * @param name The element's name
         * @param value The element's string value
         */
        public void AddEventElement(string name, string value);

        /**
         * @param element The element 
         * @param name The name of the sub element
         * @return The sub element.
         */
        public EventElement addSubelement(EventElement element, string name);

        /**
         * @param element The element
         * @param name The name of the sub element
         * @param textContent The text content of the sub element.
         */
        public void AddSubelement(EventElement element, string name, string textContent);

        /**
         * Create an event that can be populated using its reference.
         * @param source The source of the event: Ernest or user
         * @param type The event's type.
         * @param t The event's time stamp.
         * @return The pointer to the event.
         */
        public EventElement NewEvent(string source, string type, int t);
    }
}
