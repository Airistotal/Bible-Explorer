namespace BE.Infrastructure.Model
{
    using System.Collections.Generic;

    public class BibleViewInfo
    {
        public BibleViewInfo()
        {
            this.MainBible = BibleID.ASV;
            this.OtherBibles = new List<BibleID>();
            this.Book = 1;
            this.Chapter = 1;
        }

        public BibleID MainBible { get; set; }

        public List<BibleID> OtherBibles { get; set; }

        public int Book { get; set; }

        public int Chapter { get; set; }

        /// <summary>
        /// This will make sure all the properties are valid properties.
        /// Returns true if self was changed, false if not.
        /// </summary>
        /// <returns>Returns true if self was changed, false if not.</returns>
        public bool CleanSelf()
        {
            var changed = false;

            if (this.MainBible == BibleID.INVALID)
            {
                this.MainBible = BibleID.ASV;
                changed = true;
            }

            if (this.OtherBibles == null)
            {
                this.OtherBibles = new List<BibleID>();
                changed = true;
            }

            if (this.Book <= 0)
            {
                this.Book = 1;
                changed = true;
            }

            if (this.Chapter <= 0)
            {
                this.Chapter = 1;
                changed = true;
            }

            return changed;
        }
    }
}