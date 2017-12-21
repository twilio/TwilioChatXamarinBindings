namespace Com.Twilio.Chat
{
    public sealed class Paginator<T>
    {
        private InternalPaginator paginator;
        private System.Collections.Generic.IList<T> typedItems = new System.Collections.Generic.List<T>();

        public Paginator(InternalPaginator paginator)
        {
            this.paginator = paginator;
            foreach (var item in paginator.Items)
            {
                typedItems.Add((T)(object)item);
            };
        }

        public bool HasNextPage
        {
            get { return paginator.HasNextPage; }
        }

        public System.Collections.Generic.IList<T> Items
        {
            get { return this.typedItems; }
        }

        public long PageSize
        {
            get { return paginator.PageSize; }
            
        }

        public void RequestNextPage(CallbackListener<Paginator<T>> listener)
        {
            paginator.RequestNextPage(listener);
        }

    }
}
