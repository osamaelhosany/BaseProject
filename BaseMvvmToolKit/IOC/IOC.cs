namespace BaseMvvmToolKIt
{
    public class IOC
    {
        static IOC()
        {
        }

        static ITinyIOC _freshIOCContainer;

        public static ITinyIOC Container
        {
            get
            {
                if (_freshIOCContainer == null)
                    _freshIOCContainer = new FreshTinyIOCBuiltIn();

                return _freshIOCContainer;
            }
        }

        public static void OverrideContainer(ITinyIOC overrideContainer)
        {
            _freshIOCContainer = overrideContainer;
        }
    }
}

