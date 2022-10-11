public class DialogsFactory
{
    private readonly DialogsConfiguration dialogsConfiguration;

    public DialogsFactory(DialogsConfiguration dialogsConfiguration)
    {
        this.dialogsConfiguration = dialogsConfiguration;
    }
        
    public Dialog Create(string id)
    {
        var prefab = dialogsConfiguration.GetDialogPrefabById(id);

        return prefab;
    }
}