import React, { Component } from 'react';
import { connect } from "react-redux";
import {
    CButton,
    CCard,
    CCardBody,
    CCardHeader,
    CCol,
    CDataTable,
    CRow
} from '@coreui/react';
import CIcon from '@coreui/icons-react';
import { getAll, add, edit, remove } from "../../actions/anggota";

const fields = ['Nama', 'Email', 'NoTlp', 'JenisKelamin', 'Alamat', 'action'];

class ListAnggota extends Component {
    constructor(props) {
        super(props);

        this.state = {}

        this.onClickEdit = this.onClickEdit.bind(this);
        this.onClickDelete = this.onClickDelete.bind(this);
    }

    componentDidMount() {
        this.props.getAll();
    }

    onClickAdd() {
        this.props.add();
    }

    onClickEdit(id, data) {
        this.props.edit(id, data);
    }

    onClickDelete(id) {
        this.props.remove(id);
    }

    render() {
        const { data = null, isLoading } = this.props;

        return (
            <>
                <CRow>
                    <CCol xs="12" lg="12">
                        <CCard>
                            <CCardHeader>
                                <CRow>
                                    <CCol xs="12" lg="10">Daftar Anggota</CCol>
                                    <CCol xs="12" lg="2">
                                        <CButton block color="success" onClick={this.onClickAdd.bind(this)} className="pull-right">Add</CButton>
                                    </CCol>
                                </CRow>
                            </CCardHeader>
                            <CCardBody>
                                <CDataTable
                                    loading={isLoading}
                                    items={data}
                                    fields={fields}
                                    itemsPerPage={5}
                                    pagination
                                    columnFilter
                                    hover
                                    sorter
                                    scopedSlots={{
                                        'action':
                                            (item) => (
                                                <td align="center">
                                                    <CButton color={"primary"} size={"sm"} className="ml-2"
                                                        onClick={() => {
                                                            this.onClickEdit(item.Id, item);
                                                        }}>
                                                        <CIcon name="cil-pencil" title="edit" />
                                                    </CButton>
                                                    <CButton color={"danger"} size={"sm"} className="ml-2"
                                                        onClick={() => {
                                                            this.onClickDelete(item.Id);
                                                        }}>
                                                        <CIcon name="cil-trash" title="delete" />
                                                    </CButton>
                                                </td>
                                            ),
                                        'JenisKelamin':
                                            (item) => (
                                                <td>
                                                    {
                                                        (item.JenisKelamin === "L") ? "Laki-laki" : "Perempuan"
                                                    }
                                                </td>
                                            ),
                                    }}
                                />
                            </CCardBody>
                        </CCard>
                    </CCol>
                </CRow>
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    data: state.anggota.data,
    isLoading: state.anggota.isLoading
});

export default connect(
    mapStateToProps,
    {
        getAll,
        add,
        edit,
        remove
    }
)(ListAnggota);
