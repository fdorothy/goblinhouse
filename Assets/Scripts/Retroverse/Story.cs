using System.Collections.Generic;

namespace Retroverse
{
    public class Story
    {
        // each story has a top-level list of labels that point to content
        public Dictionary<string, Conversation> sections;

        public Story()
        {
            sections = new Dictionary<string, Conversation>();
        }

        public Story AddSection(string name, Conversation chapter)
        {
            sections[name] = chapter;
            return this;
        }
    }
}
