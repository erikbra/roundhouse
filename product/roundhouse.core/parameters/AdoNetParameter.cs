namespace roundhouse.parameters
{
    using System.Data;

    public class AdoNetParameter : IParameter<IDbDataParameter>
    {
        private readonly IDbDataParameter parameter;

        public AdoNetParameter(IDbDataParameter parameter)
        {
            this.parameter = parameter;
        }

        public IDbDataParameter underlying_type => parameter;

        public string name =>  parameter != null ? parameter.ParameterName : string.Empty;

        public object value => parameter != null ? parameter.Value : string.Empty;
    }
}