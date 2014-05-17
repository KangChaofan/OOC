var draw;
var dragElement = undefined;
var dragOffsetX;
var dragOffsetY;
var modelElement;
var modelLink;
var linkElement;
var linkAttribute;
var markerArrow;
var compositionGuid;
var eventBinding = {};
var models;
var links;
function beforeCreateComposition() {
    draw.clear();
    markerArrow = draw.defs().marker(13, 13);
    markerArrow.path('M-8 -7L-8 7L0 0L-8 -7Z').attr({
        'fill': '#6666ff'
    });
    markerArrow.attr({
        'markerWidth': 13,
        'markerHeight': 13,
        'refx': 0,
        'refy': 0,
        'orient': 'auto',
        'viewBox': '-8 -7 14 14'
    });
    modelElement = {};
    modelLink = {};
    linkElement = {};
    linkAttribute = {};
}
function saveCompositionModelPosition(guid) {
    var e = modelElement[guid];
    invoke('composition_set_model_position', 'guid=' + guid + '&x=' + e.x() + '&y=' + e.y());
}
function afterCreateComposition() {
    draw.mousemove(function(e) {
        if (dragElement != undefined) {
            dragElement.move(e.clientX - dragOffsetX, e.clientY - dragOffsetY);

            var guid = dragElement.attr('guid');
            if (modelLink[guid]) {
                for (var i in modelLink[guid]) {
                    updateLink(modelLink[guid][i]);
                }
            }
        }
    });
}
function setupDragging(element) {
    element.mousedown(function(e) {
        if (e.button != 0) return true;
        dragElement = this;
        dragOffsetX = e.clientX - this.x();
        dragOffsetY = e.clientY - this.y();
        this.front();
        return true;
    });
    element.mouseup(function() {
        var guid = dragElement.attr('guid');
        saveCompositionModelPosition(guid);

        dragElement = undefined;
    });
}
function setupModelListeners(element) {
    setupDragging(element);
    element.last().mouseup(function() {
        var guid = element.attr('guid');
        triggerComposition('modelClicked', {'guid': guid});
    });
}
function setupLinkListeners(element) {
    element.mousedown(function() {
        var guid = element.attr('guid');
        triggerComposition('linkClicked', {"guid": guid});
    });
}
function updateLink(guid) {
    var source = modelElement[linkAttribute[guid].sourceGuid];
    var target = modelElement[linkAttribute[guid].targetGuid];
    var midX = (source.cx() + target.cx()) / 2;
    var midY = (source.cy() + target.cy()) / 2;
    var cmd = "M" + source.cx() + "," + source.cy();
    cmd += "L" + midX + "," + midY;
    cmd += "L" + target.cx() + "," + target.cy();
    linkElement[guid].plot(cmd);
    linkElement[guid].back();
}
function createLink(guid, sourceCmGuid, targetCmGuid) {
    linkAttribute[guid] = {"sourceGuid": sourceCmGuid, "targetGuid": targetCmGuid};
    linkElement[guid] = draw.path("M0,0L1,1").style('cursor', 'pointer').stroke({ color: '#6666ff', width: 1 }).attr('marker-mid', markerArrow).attr('guid', guid);
    modelLink[sourceCmGuid].push(guid);
    modelLink[targetCmGuid].push(guid);
    updateLink(guid);
    setupLinkListeners(linkElement[guid]);
}
function createModel(guid, name, x, y) {
    var g = draw.group().attr('guid', guid);
    var rect = draw.rect(150, 40).fill('#ffffff').stroke('#000');
    g.add(rect);
    var text = draw.text(name).attr({x:75,y:8}).attr('class', 'unselectable').style('cursor', 'pointer');
    text.font({anchor: 'middle', size: 12, 'family': 'Verdana'});
    g.add(text);
    g.move(x, y);
    setupModelListeners(g);
    modelElement[guid] = g;
    modelLink[guid] = [];
}
function createComposition() {
    beforeCreateComposition();
    for (var i in models) {
        var x = 300;
        var y = 200;
        var px = readModelProperty(models[i], 'x');
        var py = readModelProperty(models[i], 'y');
        if (px) x = parseInt(px);
        if (py) y = parseInt(py);
        createModel(models[i].CompositionModel.guid, models[i].Model.name, x, y);
    }
    for (var i in links)
        createLink(links[i].CompositionLink.guid, links[i].CompositionLink.sourceCmGuid, links[i].CompositionLink.targetCmGuid);
    afterCreateComposition();
}
function reloadComposition() {
    draw.clear();
    var text = draw.text("Loading...").font({size: 20, 'family': 'Verdana'}).move(340, 210);
    invoke('composition_data', 'guid=' + compositionGuid, function(result) {
        models = [];
        links = [];
        if (result.data.Models != null)
            models = result.data.Models.CompositionModelData;
        if (result.data.Links != null)
            links = result.data.Links.CompositionLinkData;
        createComposition(models, links);
    });
}
function initComposition(guid) {
    compositionGuid = guid;
    draw = SVG('composition-svg').size(800, 600);
    reloadComposition();
}
function triggerComposition(name, evt) {
    if (eventBinding[name]) {
        eventBinding[name](evt);
    }
}
function onComposition(name, handler) {
    eventBinding[name] = handler;
}
function getModelData(guid) {
    for (var i in models) {
        if (models[i].CompositionModel.guid == guid)
            return models[i];
    }
}
function getLinkData(guid) {
    for (var i in links) {
        if (links[i].CompositionLink.guid == guid)
            return links[i];
    }
}
function getRelatedLinkData(guid) {
    var link = getLinkData(guid);
    var relatedLinks = [];
    for (var i in links) {
        if (links[i].CompositionLink.sourceCmGuid != link.CompositionLink.sourceCmGuid
            && links[i].CompositionLink.sourceCmGuid != link.CompositionLink.targetCmGuid) continue;
        if (links[i].CompositionLink.targetCmGuid != link.CompositionLink.sourceCmGuid
            && links[i].CompositionLink.targetCmGuid != link.CompositionLink.targetCmGuid) continue;
        relatedLinks.push(links[i]);
    }
    return relatedLinks;
}
function readModelProperty(model, key) {
    var kvs = model.PropertyValues.Kvs.KeyValueOfstringstring;
    for (var i in kvs) {
        if (kvs[i].Key == key) return kvs[i].Value;
    }
}