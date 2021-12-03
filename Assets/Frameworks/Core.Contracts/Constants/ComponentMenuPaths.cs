namespace VisualStudioEX3.Artemis.Framework.Core.Contracts.Constants
{
    public static class ComponentMenuPaths
    {
        #region Constants
        private const string MATERIAL_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE = CoreConstants.BASE_MENU_NAMESPACE + "Materials/";
        private const string EFFECTS_MATERIAL_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE = MATERIAL_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE + "Effects/";

        public const string SCROLL_MATERIAL_COMPONENT_MENU_NAMESPACE = EFFECTS_MATERIAL_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE + "Scroll Material";
        public const string FADE_MATERIAL_COMPONENT_MENU_NAMESPACE = EFFECTS_MATERIAL_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE + "Fade Material";

        private const string TRANSFORMS_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE = CoreConstants.BASE_MENU_NAMESPACE + "Transforms/";
        public const string ROTATE_TRANSFORM_COMPONENT_MENU_NAMESPACE = TRANSFORMS_COMPONENTS_BASE_COMPONENT_MENU_NAMESPACE + "Rotate Transform";
        #endregion
    }
}
