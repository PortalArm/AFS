hello = () => {
    console.log("hello!");
}

function addFolderInput() {
    document.getElementById("addFolder").disabled = true;
    let lastFolder = $(".c-folder").first().closest(".row");
    console.log(lastFolder);
    if (lastFolder.length === 0) {
        console.log("voshel");
        lastFolder = $("#contentDiv .row").first();
        if (lastFolder.length === 0) {
            console.log("eshe voshel");
            $("#contentDiv > div").html("<div class='dummy-one'></div>");
            lastFolder = $(".dummy-one");
            console.log(lastFolder);
        }
    }
    console.log(lastFolder);

    lastFolder.before("<div class='row'><div class='col'><input class='c-folder d-block entered-text' value='folder'/></div></div>");
    $(".dummy-one").remove();
    let enteredText = $(".entered-text");
    enteredText.focus().select();
    enteredText.on("focusout", e => {
        let folderName = $(".entered-text").val();
        $.ajax({
            method: "POST",
            url: "/Profile/AddFolder",
            data: { "name":folderName },
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
        text.parent().append(`<span class='c-folder' value='${newId}'>${folderName}</span>`);
        text.remove();
    }
    else {
        let row = text.closest(".row");
        text.parent().html(`<span class='alert-dark'>Папка с именем ${folderName} уже существует</span>`)
            .fadeOut(500, e => {
                row.remove();
            });
        
    }
    document.getElementById("addFolder").disabled = false;
}
function toggleSelection(e) {
    $(e.target).closest(".row").toggleClass("selected-object").siblings().removeClass("selected-object");
    //let addFile = document.getElementById("addFile");
    let removeFile = document.getElementById("removeFile");
    //let addFolder = document.getElementById("addFolder");
    let removeFolder = document.getElementById("removeFolder");
    if ($(e.target).closest(".row").hasClass("selected-object")) {
        //если выбран файл
        if (e.target.className.indexOf("c-file") + 1) {
            //addFile.disabled = false;
            removeFile.disabled = false;
            //addFolder.disabled = false;
            removeFolder.disabled = true;
        }
        else {
            //addFile.disabled = false;
            removeFile.disabled = true;
            //addFolder.disabled = false;
            removeFolder.disabled = false;
        }
    }
    else {
        removeFile.disabled = true;
        removeFolder.disabled = true;
    }

}
function getHistoryFromCrumbs() {
    let arr = new Array();
    let crumbs = $(".breadcrumb-item");
    for (let i = 0; i < crumbs.length; ++i) {
        let dataId = $(crumbs[i]).attr("data-id");
        let dataVal = $(crumbs[i]).attr("data-val");
        let item = new Object();
        item.id = null;
        item.value = "root";
        if (dataId != null) {
            item.id = parseInt(dataId);
            item.value = dataVal;
        }
        arr.push(item);
    }
    return arr;
}
function constructCrumbs(pHistory) {
    let outputString = "";
    for (let historyItem of pHistory)
        outputString += `<div class="breadcrumb-item" data-val="${historyItem.value}" data-id="${isNaN(historyItem.id) ? 'null' : historyItem.id}">${historyItem.value}</div>`;

    $(".breadcrumb").html(outputString);
}
function clearInfo() {
    $(".object-info").html("");
    if ($(".object-info").is(":visible"))
        $(".object-info").toggle();
}
function loadFolder(folder, newHistory, id, isFromHistory) {
    //console.log("------- passed history and history from crumbs -------");
    let oldHistory = getHistoryFromCrumbs();
    console.log("---------------------------");
    console.log("Previous history(passed as argument):");
    console.table(newHistory);

    console.log("Previous history(constructed from function):");
    console.table(oldHistory);
    if (!isFromHistory)
        history.pushState({ "hist": prevHist, "uId": defUserId, "folder": folder }, "great_title_" + Date.now());
    if (id)
        defUserId = id;
    // uhh
    if (!folder)
        folder = null;

    if (newHistory)
        constructCrumbs(newHistory);

    console.log("Current history(constructed from function):");
    console.table(getHistoryFromCrumbs());
    
    //$("#contentDiv").html("Загрузка...");
    $("#contentDiv").load("/Profile/GetContent", { "id": defUserId, "folderId": folder, "history": getHistoryFromCrumbs() });
}

