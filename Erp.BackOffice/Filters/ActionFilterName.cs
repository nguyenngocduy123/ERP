namespace Erp.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The action type.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// None action
        /// </summary>
        None = 0,
        /// <summary>
        /// The insert.
        /// </summary>
        Insert = 1, 

        /// <summary>
        /// The update.
        /// </summary>
        Update, 

        /// <summary>
        /// The delete.
        /// </summary>
        Delete, 

        /// <summary>
        /// The publish.
        /// </summary>
        Publish, 

        /// <summary>
        /// The distribution.
        /// </summary>
        Distribution
    }

    /// <summary>
    /// The action filter name.
    /// </summary>
    public class ActionFilterName
    {
        #region Fields

        /// <summary>
        /// The action name types.
        /// </summary>
        private readonly List<ActionNameType> actionNameTypes = new List<ActionNameType>
            {
                new ActionNameType { Name = "insert", Type = (int)ActionType.Insert }, 
                new ActionNameType { Name = "add", Type = (int)ActionType.Insert }, 
                new ActionNameType { Name = "create", Type = (int)ActionType.Insert }, 
                new ActionNameType { Name = "update", Type = (int)ActionType.Update }, 
                new ActionNameType { Name = "edit", Type = (int)ActionType.Update }, 
                new ActionNameType { Name = "save", Type = (int)ActionType.Update }, 
                new ActionNameType { Name = "delete", Type = (int)ActionType.Delete }, 
                new ActionNameType { Name = "remove", Type = (int)ActionType.Delete }, 
                new ActionNameType { Name = "publish", Type = (int)ActionType.Publish }, 
                new ActionNameType { Name = "distribution", Type = (int)ActionType.Distribution }
            };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get action type.
        /// </summary>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetActionType(string actionName)
        {
            actionName = actionName.ToLower(System.Globalization.CultureInfo.CurrentCulture);

            ActionNameType action = this.actionNameTypes.FirstOrDefault(a => actionName.Contains(a.Name));
            if (action != null)
            {
                return action.Type;
            }

            return 0;
        }

        #endregion
    }

    /// <summary>
    /// The action name type.
    /// </summary>
    public class ActionNameType
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type { get; set; }

        #endregion
    }
}