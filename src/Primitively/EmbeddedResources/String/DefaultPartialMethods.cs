    // These partial methods allow for customisations before and after
    // the IsMatch method call during initialisation
    static partial void PreMatchCheck(ref string value);
    static partial void PostMatchCheck(ref string value);
