import {
    GET_DATA_PENERBIT,
    SET_DATA_PENERBIT,
    GET_LIST_PENERBIT,
    SET_LIST_PENERBIT,
    SET_ADD_PENERBIT,
    SET_EDIT_PENERBIT,
    DELETE_PENERBIT,
    SAVE_PENERBIT,
    BACK_TO_LIST_PENERBIT,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "penerbit",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data PENERBIT"),
        label: GET_LIST_PENERBIT
    });
}

export function getById(id) {
    return apiAction({
        url: "penerbit/" + id,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data PENERBIT"),
        label: GET_DATA_PENERBIT
    });
}

export function save(param, id = 0) {
    let _url = "penerbit";
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
        onFailure: (error) => console.log("Error occured loading get data PENERBIT"),
        label: SAVE_PENERBIT
    });
}

function setList(data) {
    return {
        type: SET_LIST_PENERBIT,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_PENERBIT,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_PENERBIT,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_PENERBIT,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(id, data, disabled = false) {
    return {
        type: SET_EDIT_PENERBIT,
        payload: data,
        dataId: id,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(id) {
    return apiAction({
        url: "penerbit/" + id,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data PENERBIT"),
        label: DELETE_PENERBIT
    });
}