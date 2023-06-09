namespace ArenaGame.Base
{
    public interface IComponent<TModel>
        where TModel : BaseModel
    {
        public void OnUpdate(TModel model);

        public TModel GetModel();
    }
}