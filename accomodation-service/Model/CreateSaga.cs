namespace accomodation_service.Model
{
    public class CreateSaga
    {
        public Func<Task<bool>> Step1 { get; set; }
        public Func<Task<bool>> Step2 { get; set; }
        public Func<Task<bool>> Step3 { get; set; }
        public Func<Task> RbStep2 { get; set; }
        public Func<Task> RbStep3 { get; set; }
    }
}
