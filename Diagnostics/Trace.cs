namespace Ideal.Diagnostics
{
    public class Trace
    {
        private static Tracer<Element> tracer;
        private static int time = 0;

        public static void Init(Tracer<Element> t)
        {
            tracer = t;
        }

        public static void StartNewEvent()
        {
            if (tracer != null)
                tracer.StartNewEvent(time++);
        }

        public static Element AddEventElement(string name)
        {
            if (tracer != null)
                return tracer.AddEventElement(name);
            else
                return null;
        }

        public static void AddEventElement(string name, string value)
        {
            if (tracer != null)
                tracer.AddEventElement(name, value);
        }

        public static Element AddSubelement(Element element, string name)
        {
            if (tracer != null)
                return tracer.AddSubelement(element, name);
            else
                return null;
        }

        public static void AddSubelement(Element element, string name, string textContent)
        {
            if (tracer != null)
                tracer.AddSubelement(element, name, textContent);
        }
    }
}
