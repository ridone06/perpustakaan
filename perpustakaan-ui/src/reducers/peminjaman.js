import {
    GET_DATA_PEMINJAMAN,
    SET_DATA_PEMINJAMAN,
    GET_LIST_PEMINJAMAN,
    SET_LIST_PEMINJAMAN,
    SET_ADD_PEMINJAMAN,
    SET_EDIT_PEMINJAMAN,
    SET_PENGEMBALIAN,
    SAVE_PEMINJAMAN,
    DELETE_PEMINJAMAN,
    BACK_TO_LIST_PEMINJAMAN,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function peminjamanReducer(state = initialState, { type, payload, pageMode, dataId, formId, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_PEMINJAMAN:
        case SET_LIST_PEMINJAMAN:
        case BACK_TO_LIST_PEMINJAMAN:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_PEMINJAMAN:
        case SET_EDIT_PEMINJAMAN:
        case SET_PENGEMBALIAN:
            return {
                data: payload,
                pageMode: pageMode,
                dataId: dataId || 0,
                formId: formId || "peminjaman",
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_PEMINJAMAN:
                    case GET_LIST_PEMINJAMAN:
                    case SAVE_PEMINJAMAN:
                    case DELETE_PEMINJAMAN:
                        return {
                            ...state,
                            isLoading: true
                        };
                    default:
                        return state;
                }
            }
        case API_ERROR:
        case API_END:
            {
                switch (payload) {
                    case GET_DATA_PEMINJAMAN:
                    case GET_LIST_PEMINJAMAN:
                    case SAVE_PEMINJAMAN:
                    case DELETE_PEMINJAMAN:
                        return {
                            ...state,
                            error: error,
                            isLoading: false
                        };
                    default:
                        return state;
                }
            }
        default:
            return state;
    }
}