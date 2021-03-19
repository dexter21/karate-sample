namespace api.Items
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Item() { }

        public Item(string name)
            => Name = name;
    }
}