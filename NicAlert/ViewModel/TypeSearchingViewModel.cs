namespace NicAlert.ViewModel
{
    public class TypeSearchingViewModel
    {
        public string Text { get; set; }

        public TypeSearching Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}