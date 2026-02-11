using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.GenericInterfaces;

public abstract class BizActionErrors
{
    private readonly List<ValidationResult> _errors = new List<ValidationResult>(); //#B

    public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList(); //#C

    public bool HasErrors => _errors.Any(); //#D

    protected void AddError(string errorMessage, params string[] propertyNames) //#E
    {
        _errors.Add(new ValidationResult (errorMessage, propertyNames)); //#F
    }
}

/***********************************************************
    #A This is an abstract class that provides error handling for business logic
    #B Hold the list of validation errors privately
    #C But I provide a public, immutable list of errors to the public
    #D A simple bool HasErrors to make checking for errors easier
    #E I created this method to allow a simple error message, or an error message with properties linked to it, to be added to the errors list
    #F The validation result has an error message and a, possibly empty, list of properties it is linked to
     * *********************************************************/