using System.Collections.Generic;

namespace Retroverse
{
    [System.Serializable]
    public enum ConversationActionType
    {
        NONE, LINE, FUNCTION, DELAY
    }

    [System.Serializable]
    public class ConversationAction
    {
        public ConversationActionType type;
        public Line line;
        public System.Action function;
        public float delay;
    }

    [System.Serializable]
    public class Conversation
    {
        public List<ConversationAction> actions;

        static public Conversation Begin() { return new Conversation(); }

        public Conversation()
        {
            this.actions = new List<ConversationAction>();
        }

        public Conversation(ConversationAction[] actions)
        {
            this.actions = new List<ConversationAction>(actions);
        }

        public Conversation Line(string text, string actor = "main")
        {
            ConversationAction action = new ConversationAction();
            action.type = ConversationActionType.LINE;
            action.line = new Line(text, actor);
            actions.Add(action);
            return this;
        }

        public Conversation Function(System.Action function)
        {
            ConversationAction action = new ConversationAction();
            action.function = function;
            action.type = ConversationActionType.FUNCTION;
            actions.Add(action);
            return this;
        }
    }
}