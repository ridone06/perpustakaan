import {
    GET_DATA_PENGEMBALIAN,
    SET_DATA_PENGEMBALIAN,
    GET_LIST_PENGEMBALIAN,
    SET_LIST_PENGEMBALIAN,
    SET_ADD_PENGEMBALIAN,
    SET_EDIT_PENGEMBALIAN,
    DELETE_PENGEMBALIAN,
    SAVE_PENGEMBALIAN,
    BACK_TO_LIST_PENGEMBALIAN,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "pengembalian",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data PENGEMBALIAN"),
        label: GET_LIST_PENGEMBALIAN
    });
}

export function getById(id) {
    return apiAction({
        url: "pengembalian/" + id,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data PENGEMBALIAN"),
        label: GET_DATA_PENGEMBALIAN
    });
}

export function save(param, id = 0) {
    let _url = "pengembalian";
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
        onFailure: (error) => console.log("Error occured loading get data PENGEMBALIAN"),
        label: SAVE_PENGEMBALIAN
    });
}

function setList(data) {
    return {
        type: SET_LIST_PENGEMBALIAN,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_PENGEMBALIAN,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_PENGEMBALIAN,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_PENGEMBALIAN,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(id, data, disabled = false) {
    return {
        type: SET_EDIT_PENGEMBALIAN,
        payload: data,
        dataId: id,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(id) {
    return apiAction({
        url: "pengembalian/" + id,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data buku"),
        label: DELETE_PENGEMBALIAN
    });
}