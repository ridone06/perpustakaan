import {
    GET_DATA_ANGGOTA,
    SET_DATA_ANGGOTA,
    GET_LIST_ANGGOTA,
    SET_LIST_ANGGOTA,
    SET_ADD_ANGGOTA,
    SET_EDIT_ANGGOTA,
    SAVE_ANGGOTA,
    DELETE_ANGGOTA,
    BACK_TO_LIST_ANGGOTA,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function anggotaReducer(state = initialState, { type, payload, pageMode, dataId, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_ANGGOTA:
        case SET_LIST_ANGGOTA:
        case BACK_TO_LIST_ANGGOTA:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_ANGGOTA:
        case SET_EDIT_ANGGOTA:
            return {
                data: payload,
                pageMode: pageMode,
                dataId: dataId || 0,
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_ANGGOTA:
                    case GET_LIST_ANGGOTA:
                    case SAVE_ANGGOTA:
                    case DELETE_ANGGOTA:
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
                    case GET_DATA_ANGGOTA:
                    case GET_LIST_ANGGOTA:
                    case SAVE_ANGGOTA:
                    case DELETE_ANGGOTA:
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