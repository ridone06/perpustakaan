import {
    GET_DATA_PEMINJAMAN,
    SET_DATA_PEMINJAMAN,
    GET_LIST_PEMINJAMAN,
    SET_LIST_PEMINJAMAN,
    SET_ADD_PEMINJAMAN,
    SET_EDIT_PEMINJAMAN,
    SET_PENGEMBALIAN,
    DELETE_PEMINJAMAN,
    SAVE_PEMINJAMAN,
    SAVE_PENGEMBALIAN,
    BACK_TO_LIST_PEMINJAMAN,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "peminjaman",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data peminjaman"),
        label: GET_LIST_PEMINJAMAN
    });
}

export function getById(id) {
    return apiAction({
        url: "peminjaman/" + id,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data peminjaman"),
        label: GET_DATA_PEMINJAMAN
    });
}

export function save(param, id = 0) {
    let _url = "peminjaman";
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
        onFailure: (error) => console.log("Error occured loading get data peminjaman"),
        label: SAVE_PEMINJAMAN
    });
}

export function savePengembalian(param, id = 0) {
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
        type: SET_LIST_PEMINJAMAN,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_PEMINJAMAN,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_PEMINJAMAN,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_PEMINJAMAN,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(id, data, disabled = false) {
    return {
        type: SET_EDIT_PEMINJAMAN,
        payload: data,
        dataId: id,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(id) {
    return apiAction({
        url: "peminjaman/" + id,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data buku"),
        label: DELETE_PEMINJAMAN
    });
}

export function setPengembalian(data, disabled = false) {
    return {
        type: SET_PENGEMBALIAN,
        payload: data,
        formId: "pengembalian",
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}