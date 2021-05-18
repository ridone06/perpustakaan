import {
    GET_DATA_RAK,
    SET_DATA_RAK,
    GET_LIST_RAK,
    SET_LIST_RAK,
    SET_ADD_RAK,
    SET_EDIT_RAK,
    DELETE_RAK,
    SAVE_RAK,
    BACK_TO_LIST_RAK,
    PAGE_MODE_LIST,
    PAGE_MODE_FORM
} from "./types";
import { apiAction } from "./api";

export function getAll() {
    return apiAction({
        url: "rak",
        method: "GET",
        onSuccess: setList,
        onFailure: (error) => console.log("Error occured loading get data RAK"),
        label: GET_LIST_RAK
    });
}

export function getByKode(kode) {
    return apiAction({
        url: "rak/" + kode,
        method: "GET",
        onSuccess: setData,
        onFailure: (error) => console.log("Error occured loading get data RAK"),
        label: GET_DATA_RAK
    });
}

export function save(param, kode = "") {
    let _url = "rak";
    let method = "POST"

    if (kode !== null && kode !== "") {
        _url = _url + "/" + kode;
        method = "PUT"
    }
    return apiAction({
        url: _url,
        method: method,
        data: param,
        onSuccess: backToList,
        onFailure: (error) => console.log("Error occured loading get data RAK"),
        label: SAVE_RAK
    });
}

function setList(data) {
    return {
        type: SET_LIST_RAK,
        payload: data,
        pageMode: PAGE_MODE_LIST
    };
}

function setData(data) {
    return {
        type: SET_DATA_RAK,
        payload: data,
        pageMode: PAGE_MODE_FORM
    };
}

export function backToList(data) {
    return {
        type: BACK_TO_LIST_RAK,
        payload: null,
        pageMode: PAGE_MODE_LIST
    };
}

export function add(data) {
    return {
        type: SET_ADD_RAK,
        payload: data,
        disabled: false,
        pageMode: PAGE_MODE_FORM
    };
}

export function edit(kode, data, disabled = false) {
    return {
        type: SET_EDIT_RAK,
        payload: data,
        kode: kode,
        disabled: disabled,
        pageMode: PAGE_MODE_FORM
    };
}

export function remove(kode) {
    return apiAction({
        url: "rak/" + kode,
        method: "DELETE",
        onSuccess: getAll,
        onFailure: (error) => console.log("Error occured loading get data RAK"),
        label: DELETE_RAK
    });
}