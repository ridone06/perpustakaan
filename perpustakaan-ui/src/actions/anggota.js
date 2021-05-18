import {
    GET_DATA_ANGGOTA,
    SET_DATA_ANGGOTA,
    GET_LIST_ANGGOTA,
    SET_LIST_ANGGOTA,
    SET_ADD_ANGGOTA,
    SET_EDIT_ANGGOTA,
    DELETE_ANGGOTA,
    SAVE_ANGGOTA,
    BACK_TO_LIST_ANGGOTA,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "anggota",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data ANGGOTA"),
        label: GET_LIST_ANGGOTA
    });
}

export function getById(id) {
    return apiAction({
        url: "anggota/" + id,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data ANGGOTA"),
        label: GET_DATA_ANGGOTA
    });
}

export function save(param, id = 0) {
    let _url = "anggota";
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
        onFailure: (error) => console.log("Error occured loading get data ANGGOTA"),
        label: SAVE_ANGGOTA
    });
}

function setList(data) {
    return {
        type: SET_LIST_ANGGOTA,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_ANGGOTA,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_ANGGOTA,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_ANGGOTA,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(id, data, disabled = false) {
    return {
        type: SET_EDIT_ANGGOTA,
        payload: data,
        dataId: id,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(id) {
    return apiAction({
        url: "anggota/" + id,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data buku"),
        label: DELETE_ANGGOTA
    });
}