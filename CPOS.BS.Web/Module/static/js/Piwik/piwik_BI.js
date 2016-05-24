
//
// Piwik Tracker
// LM,2016/04/13
/// <reference path="~/Contents/JS/piwik.js" />
/// <reference path="~/Contents/JS/jquery-1.12.3.js" />
//

$(function () {
    (function (jq) {
        //
        var $ = jq;

        //
        var $piwikUrl = $("input:hidden.piwik_piwikUrl").val(),
              $siteId = $("input:hidden.piwik_siteId").val(),
              $trackGoals = $("input:hidden.piwik_trackGoal").val() || "",
              $ignoreClasses = $("input:hidden.piwik_ignoreClass").val() || "",
              $itemSkus = $("input:hidden.piwik_itemSku").val() || "";

        //
        (function (url, id, goals) {
            var piwik = Piwik.getTracker(url, id);
            if (goals.length > 0) {
                var arr = goals.split(',');
                for (var i = 0; i < arr.length; i++) {
                    piwik.trackGoal(arr[i]);
                }
            }
        })($piwikUrl, $siteId, $trackGoals);

        //
        (function (url, id, classes, items) {
            var piwik = Piwik.getTracker(url, id);
            if (items.length > 0) {
                var arr = items.split(',');
                if (classes.length > 0) {
                    piwik.setIgnoreClasses(classes.split(','));
                }
                piwik.enableLinkTracking(true);
                piwik.trackPageView();
                piwik.setEcommerceView(arr[0], arr[1], arr[2], arr[3]);
            }
        })($piwikUrl, $siteId, $ignoreClasses, $itemSkus);

        //
        (function (url, id, classes, items) {
            var piwik = Piwik.getTracker(url, id);
            if (items.length == 0) {
                if (classes.length > 0) {
                    piwik.setIgnoreClasses(classes.split(','));
                }
                piwik.enableLinkTracking(true);
                piwik.trackPageView();
            }
        })($piwikUrl, $siteId, $ignoreClasses, $itemSkus);
    })(jQuery);
});