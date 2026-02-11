using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.GenericInterfaces;

public interface IBizAction<in TIn, out TOut> // #A
{
    IImmutableList<ValidationResult> Errors { get; }      //#B

    bool HasErrors { get; }  //#B
    TOut Action(TIn dto); //#C
}

    /****************************************************
    #A The BizAction uses the TIn and an TOut to define of the Action method
    #B This returns the error information from the business logic
    #C This is the action that the BizRunner will call
     * *************************************************/