namespace CefDotnet.CefApi.Types;

///
/// Transition type for a request. Made up of one source value and 0 or more
/// qualifiers.
///
public enum cef_transition_type_t : uint
{
    ///
    /// Source is a link click or the JavaScript window.open function. This is
    /// also the default value for requests like sub-resource loads that are not
    /// navigations.
    ///
    TT_LINK,

    ///
    /// Source is some other "explicit" navigation. This is the default value for
    /// navigations where the actual type is unknown. See also
    /// TT_DIRECT_LOAD_FLAG.
    ///
    TT_EXPLICIT,

    ///
    /// User got to this page through a suggestion in the UI (for example, via the
    /// destinations page). Chrome style only.
    ///
    TT_AUTO_BOOKMARK,

    ///
    /// Source is a subframe navigation. This is any content that is automatically
    /// loaded in a non-toplevel frame. For example, if a page consists of several
    /// frames containing ads, those ad URLs will have this transition type.
    /// The user may not even realize the content in these pages is a separate
    /// frame, so may not care about the URL.
    ///
    TT_AUTO_SUBFRAME,

    ///
    /// Source is a subframe navigation explicitly requested by the user that will
    /// generate new navigation entries in the back/forward list. These are
    /// probably more important than frames that were automatically loaded in
    /// the background because the user probably cares about the fact that this
    /// link was loaded.
    ///
    TT_MANUAL_SUBFRAME,

    ///
    /// User got to this page by typing in the URL bar and selecting an entry
    /// that did not look like a URL.  For example, a match might have the URL
    /// of a Google search result page, but appear like "Search Google for ...".
    /// These are not quite the same as EXPLICIT navigations because the user
    /// didn't type or see the destination URL. Chrome style only.
    /// See also TT_KEYWORD.
    ///
    TT_GENERATED,

    ///
    /// This is a toplevel navigation. This is any content that is automatically
    /// loaded in a toplevel frame.  For example, opening a tab to show the ASH
    /// screen saver, opening the devtools window, opening the NTP after the safe
    /// browsing warning, opening web-based dialog boxes are examples of
    /// AUTO_TOPLEVEL navigations. Chrome style only.
    ///
    TT_AUTO_TOPLEVEL,

    ///
    /// Source is a form submission by the user. NOTE: In some situations
    /// submitting a form does not result in this transition type. This can happen
    /// if the form uses a script to submit the contents.
    ///
    TT_FORM_SUBMIT,

    ///
    /// Source is a "reload" of the page via the Reload function or by re-visiting
    /// the same URL. NOTE: This is distinct from the concept of whether a
    /// particular load uses "reload semantics" (i.e. bypasses cached data).
    ///
    TT_RELOAD,

    ///
    /// The url was generated from a replaceable keyword other than the default
    /// search provider. If the user types a keyword (which also applies to
    /// tab-to-search) in the omnibox this qualifier is applied to the transition
    /// type of the generated url. TemplateURLModel then may generate an
    /// additional visit with a transition type of TT_KEYWORD_GENERATED against
    /// the url 'http://' + keyword. For example, if you do a tab-to-search
    /// against wikipedia the generated url has a transition qualifer of
    /// TT_KEYWORD, and TemplateURLModel generates a visit for 'wikipedia.org'
    /// with a transition type of TT_KEYWORD_GENERATED. Chrome style only.
    ///
    TT_KEYWORD,

    ///
    /// Corresponds to a visit generated for a keyword. See description of
    /// TT_KEYWORD for more details. Chrome style only.
    ///
    TT_KEYWORD_GENERATED,

    TT_NUM_VALUES,

    ///
    /// General mask defining the bits used for the source values.
    ///
    TT_SOURCE_MASK = 0xFF,

    /// Qualifiers.
    /// Any of the core values above can be augmented by one or more qualifiers.
    /// These qualifiers further define the transition.

    ///
    /// Attempted to visit a URL but was blocked.
    ///
    TT_BLOCKED_FLAG = 0x00800000,

    ///
    /// Used the Forward or Back function to navigate among browsing history.
    /// Will be ORed to the transition type for the original load.
    ///
    TT_FORWARD_BACK_FLAG = 0x01000000,

    ///
    /// Loaded a URL directly via CreateBrowser, LoadURL or LoadRequest.
    ///
    TT_DIRECT_LOAD_FLAG = 0x02000000,

    ///
    /// User is navigating to the home page. Chrome style only.
    ///
    TT_HOME_PAGE_FLAG = 0x04000000,

    ///
    /// The transition originated from an external application; the exact
    /// definition of this is embedder dependent. Chrome style only.
    ///
    TT_FROM_API_FLAG = 0x08000000,

    ///
    /// The beginning of a navigation chain.
    ///
    TT_CHAIN_START_FLAG = 0x10000000,

    ///
    /// The last transition in a redirect chain.
    ///
    TT_CHAIN_END_FLAG = 0x20000000,

    ///
    /// Redirects caused by JavaScript or a meta refresh tag on the page.
    ///
    TT_CLIENT_REDIRECT_FLAG = 0x40000000,

    ///
    /// Redirects sent from the server by HTTP headers.
    ///
    TT_SERVER_REDIRECT_FLAG = 0x80000000,

    ///
    /// Used to test whether a transition involves a redirect.
    ///
    TT_IS_REDIRECT_MASK = 0xC0000000,

    ///
    /// General mask defining the bits used for the qualifiers.
    ///
    TT_QUALIFIER_MASK = 0xFFFFFF00,
}