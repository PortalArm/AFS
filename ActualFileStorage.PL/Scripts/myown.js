hello = () => {
    console.log("hello!");
}

actBtns = (btnsIds, isEnable) => {
    for (let btnId of btnsIds) {
        let btn;
        if (btn = document.getElementById(btnId))
            btn.disabled = !isEnable;
    }
}
//var lastTarg;
function sendDataOnAccessChange($target) {
    //lastTarg = $target;
    //console.log(target.closest("#infoDiv").children("#fileIdDiv"));
    if (!$target)
        return;
    let $baseFolder = $target.closest("#infoDiv").filter(":visible");
    let file = $baseFolder.children("#fileIdDiv");
    let folder = $baseFolder.children("#folderIdDiv");
    console.log(file);
    console.log(folder);
    if (file.length === 0 && folder.length === 0)
        return;

    let id = file.length === 0 ? folder.html() : file.html();
    let lvl = $target.val();
    //console.log("Сделать изменение уровня доступа при изменении");
    let passedData = { "objectId": id, "level": lvl, "isFile": file.length !== 0 };
    console.log(passedData);
    $.ajax({
        method: "POST",
        url: "/Profile/ChangeAccess",
        data: passedData,
        success: d => {
            console.log(d);
        }
    });
}

function addLinkInput($lnkTarget) {
    $lnkTarget.parent().parent().append("<div class='col'><input class='d-block entered-text-link' value='link'/></div>");
    $lnkTarget.hide();
    
    let textLink = $lnkTarget.closest("#infoDiv").find(".entered-text-link");//$(".entered-text-link");
    //let fileId = $("#fileIdDiv").html();
    //let folderId = $("#folderIdDiv").html();
    //console.log($lnkTarget.closest("#infoDiv"));
    let $baseFolder = $lnkTarget.closest("#infoDiv").filter(":visible");
    let file = $baseFolder.children("#fileIdDiv");
    let folder = $baseFolder.children("#folderIdDiv");
    //console.log(file);
    //console.log(folder);
    //console.log(textLink);
    if (file.length === 0 && folder.length === 0)
        return;

    let id = file.length === 0 ? folder.html() : file.html();
    textLink.focus().select();
    textLink
        .on("keydown", e => {
            if (e.keyCode === 13) {//enter
                textLink.blur();
            }
            else if (e.keyCode === 27) {//escape
                $lnkTarget.show();
                textLink.closest("div .col").remove();
            }
        })
        .on("focusout", e => {
            let text = textLink.val();
            //console.log(fileId);
            $.ajax({
                method: "POST",
                data: { "objectId": id, "link": text, "isFile": file.length !== 0 },
                url: "/Profile/MakeLink",
                success: d => {
                    //d = {link: "newlinkhaha", status: true, error: "oshibka"};
                    convertToLink($lnkTarget, d.link, d.status, d.errorMessage);
                }
            });
        });
}

function convertToLink($target, link, isCorrect, errorMessage) {
    let linkText = $target.closest("#infoDiv").find(".entered-text-link").parent();
    console.log(linkText);
    console.log($target);
    if (!isCorrect) {
        linkText.html(`<span class='alert-dark'>${errorMessage}</span>`)
            .fadeOut(1000, e => {
                linkText.remove();
            });
        $target.show();
        return;
    }
    
    linkText.html(`<a href='/s/${link}'>${link}</a>`);
}
function addFolderInput() {
    actBtns(["addFolder"], false);
    //document.getElementById("addFolder").disabled = true;
    let lastFolder = $(".c-folder").first().closest(".row");
    if (lastFolder.length === 0) {
        lastFolder = $("#contentDiv .row").first();
        if (lastFolder.length === 0) {
            $("#contentDiv > div").html("<div class='dummy-one'></div>");
            lastFolder = $(".dummy-one");
        }
    }
    //console.log(lastFolder);

    lastFolder.before("<div class='row'><div class='col'><input class='c-folder d-block entered-text' value='folder'/></div></div>");
    $(".dummy-one").remove();
    let enteredText = $(".entered-text");
    enteredText.focus().select();
    enteredText.on("focusout", e => {
        let folderName = $(".entered-text").val();
        $.ajax({
            method: "POST",
            url: "/Profile/AddFolder",
            data: { "name": folderName },
            success: data => {
                convertToFolder(folderName, data.id, data.status);
            }
        });
    }).on("keydown", e => {
        if (e.keyCode === 13)//enter
            $(".entered-text").blur();
        else if (e.keyCode === 27) {//escape
            enteredText.off("focusout");
            enteredText.off("keydown");
            enteredText.closest(".row").remove();
            document.getElementById("addFolder").disabled = false;
        }
    });
}

function convertToFolder(folderName, newId, isCorrect) {
    let text = $(".entered-text");
    //let folderName = text.val();
    text.off("keydown");
    text.off("focusout");
    if (isCorrect) {
        text.parent().append(`<span class='c-folder' value='${newId}'><img src="/Content/folder-icon.png" width="12" />${folderName}</span>`);
        text.remove();
    }
    else {
        let row = text.closest(".row");
        text.parent().html(`<span class='alert-dark'>Папка с именем ${folderName} уже существует</span>`)
            .fadeOut(500, e => {
                row.remove();
            });

    }
    actBtns(["addFolder"], true);
    //document.getElementById("addFolder").disabled = false;
}
function toggleSelection(e) {
    $(e.target).closest(".row").toggleClass("selected-object").siblings().removeClass("selected-object");
    //let removeFile = document.getElementById("removeFile");
    //let removeFolder = document.getElementById("removeFolder");
    if ($(e.target).closest(".row").hasClass("selected-object")) {
        //если выбран файл
        if (e.target.className.indexOf("c-file") + 1) {
            actBtns(["removeFile"], true);
            actBtns(["removeFolder"], false);
            //removeFile.disabled = false;
            //removeFolder.disabled = true;
        }
        else {
            actBtns(["removeFile"], false);
            actBtns(["removeFolder"], true);
            //removeFile.disabled = true;
            //removeFolder.disabled = false;
        }
    }
    else {
        actBtns(["removeFile"], false);
        actBtns(["removeFolder"], false);
        //removeFile.disabled = true;
        //removeFolder.disabled = true;
    }

}
//function getHistoryFromCrumbs() {
//    let arr = new Array();
//    let crumbs = $(".breadcrumb-item");
//    for (let i = 0; i < crumbs.length; ++i) {
//        let dataId = $(crumbs[i]).attr("data-id");
//        let dataVal = $(crumbs[i]).attr("data-val");
//        let item = new Object();
//        item.id = null;
//        item.value = "root";
//        if (dataId != null) {
//            item.id = parseInt(dataId);
//            item.value = dataVal;
//        }
//        arr.push(item);
//    }
//    return arr;
//}
function constructCrumbs(pHistory) {
    let outputString = "";
    for (let historyItem of pHistory)
        outputString += `<div class="breadcrumb-item" data-val="${historyItem.Value}" data-id="${isNaN(historyItem.Id) ? 'null' : historyItem.Id}">${historyItem.Value}</div>`;

    $(".breadcrumb").html(outputString);
}
function clearInfo() {
    $(".object-info").html("");
    if ($(".object-info").is(":visible"))
        $(".object-info").toggle();
}
function loadFolder(folder, id, isFromHistory) {
    //console.log(folder + " " + id + " " + isFromHistory);

    if (!isFromHistory)
        history.pushState({ "uId": defUserId, "folder": folder }, "great_title_" + Date.now());
    if (id)
        defUserId = id;
    // uhh
    if (!folder)
        folder = null;

    //if (newHistory)
    //    constructCrumbs(newHistory);

    //$("#contentDiv").html("Загрузка...");

    $("#contentDiv").load("/Profile/GetContent", { "id": defUserId, "folderId": folder/*, "history": getHistoryFromCrumbs() */ }, (text, status, jqXHR) => {
        $("#contentDiv").find("script").remove();
    });
}

