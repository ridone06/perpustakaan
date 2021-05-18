import {
    GET_DATA_PENGARANG,
    SET_DATA_PENGARANG,
    GET_LIST_PENGARANG,
    SET_LIST_PENGARANG,
    SET_ADD_PENGARANG,
    SET_EDIT_PENGARANG,
    DELETE_PENGARANG,
    SAVE_PENGARANG,
    BACK_TO_LIST_PENGARANG,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "pengarang",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data PENGARANG"),
        label: GET_LIST_PENGARANG
    });
}

export function getById(id) {
    return apiAction({
        url: "pengarang/" + id,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data PENGARANG"),
        label: GET_DATA_PENGARANG
    });
}

export function save(param, id = 0) {
    let _url = "pengarang";
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
        onFailure: (error) => console.log("Error occured loading get data PENGARANG"),
        label: SAVE_PENGARANG
    });
}

function setList(data) {
    return {
        type: SET_LIST_PENGARANG,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_PENGARANG,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_PENGARANG,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_PENGARANG,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(id, data, disabled = false) {
    return {
        type: SET_EDIT_PENGARANG,
        payload: data,
        dataId: id,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(id) {
    return apiAction({
        url: "pengarang/" + id,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data pengarang"),
        label: DELETE_PENGARANG
    });
}