import {
    GET_DATA_BUKU,
    SET_DATA_BUKU,
    GET_LIST_BUKU,
    SET_LIST_BUKU,
    SET_ADD_BUKU,
    SET_EDIT_BUKU,
    SAVE_BUKU,
    DELETE_BUKU,
    BACK_TO_LIST_BUKU,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function bukuReducer(state = initialState, { type, payload, pageMode, dataId, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_BUKU:
        case SET_LIST_BUKU:
        case BACK_TO_LIST_BUKU:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_BUKU:
        case SET_EDIT_BUKU:
            return {
                data: payload,
                pageMode: pageMode,
                dataId: dataId || 0,
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_BUKU:
                    case GET_LIST_BUKU:
                    case SAVE_BUKU:
                    case DELETE_BUKU:
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
                    case GET_DATA_BUKU:
                    case GET_LIST_BUKU:
                    case SAVE_BUKU:
                    case DELETE_BUKU:
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