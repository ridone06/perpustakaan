import {
    GET_DATA_BUKU,
    SET_DATA_BUKU,
    GET_LIST_BUKU,
    SET_LIST_BUKU,
    SET_ADD_BUKU,
    SET_EDIT_BUKU,
    DELETE_BUKU,
    SAVE_BUKU,
    BACK_TO_LIST_BUKU,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "buku",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data BUKU"),
        label: GET_LIST_BUKU
    });
}

export function getById(id) {
    return apiAction({
        url: "buku/" + id,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data BUKU"),
        label: GET_DATA_BUKU
    });
}

export function save(param, id = 0) {
    let _url = "buku";
    let method = "POST"

    if (id > 0) {
        _url = _url + "/" + id;
        method = "PUT"
    }
    return apiAction({
        url: _url,
        method: method,
        data: param,
        onSuccess: backToList,
        onFailure: (error) => console.log("Error occured loading get data BUKU"),
        label: SAVE_BUKU
    });
}

function setList(data) {
    return {
        type: SET_LIST_BUKU,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_BUKU,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_BUKU,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_BUKU,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(id, data, disabled = false) {
    return {
        type: SET_EDIT_BUKU,
        payload: data,
        dataId: id,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(id) {
    return apiAction({
        url: "buku/" + id,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data buku"),
        label: DELETE_BUKU
    });
}