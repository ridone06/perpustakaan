import React, { Component } from 'react';
import { connect } from "react-redux";
import {
    CBadge,
    CButton,
    CCard,
    CCardBody,
    CCardHeader,
    CCol,
    CDataTable,
    CRow
} from '@coreui/react';
import CIcon from '@coreui/icons-react';
import Moment from "react-moment";
import { getAll, add, edit, remove, setPengembalian } from "../../actions/peminjaman";

const fields = [
    { key: 'NamaAnggota', label: 'Anggota' },
    { key: 'TanggalPinjam', label: 'Tanggal Pinjam' },
    { key: 'TanggalKembali', label: 'Tanggal Kembali' },
    { key: 'TanggalPengembalian', label: 'Tanggal Pengembalian' },
    { key: 'Status', label: 'Status' },
    { key: 'NamaPetugas', label: 'Petugas' },
    { key: 'action', label: 'action', _style: { width: '26%' } }];

const getBadge = status => {
    switch (status) {
        case 'Be returned': return 'success'
        case 'Overdue': return 'danger'
        default: return 'primary'
    }
}

class ListPeminjaman extends Component {
    constructor(props) {
        super(props);

        this.state = {}

        this.onClickEdit = this.onClickEdit.bind(this);
        this.onClickDelete = this.onClickDelete.bind(this);
        this.onClickView = this.onClickView.bind(this);
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

    onClickView(id, data) {
        this.props.edit(id, data, true);
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
                                    <CCol xs="12" lg="10">Daftar Peminjaman</CCol>
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
                                                    {
                                                        (item.Status !== "Be returned") ?
                                                            <CButton color={"primary"} size={"sm"} className="ml-2"
                                                                onClick={() => {
                                                                    this.onClickEdit(item.Id, item);
                                                                }}>
                                                                <CIcon name="cil-pencil" title="edit" />
                                                            </CButton>
                                                            :
                                                            null
                                                    }
                                                    {
                                                        (item.Status !== "Be returned") ?
                                                            <CButton color={"danger"} size={"sm"} className="ml-2"
                                                                onClick={() => {
                                                                    this.onClickDelete(item.Id);
                                                                }}>
                                                                <CIcon name="cil-trash" title="delete" />
                                                            </CButton>
                                                            :
                                                            null
                                                    }
                                                    <CButton color={"info"} size={"sm"} className="ml-2"
                                                        onClick={() => {
                                                            this.onClickView(item.Id, item);
                                                        }}>
                                                        <CIcon name="cil-magnifying-glass" title="view" />
                                                    </CButton>
                                                    {
                                                        (item.Status !== "Be returned") ?
                                                            <CButton color={"success"} size={"sm"} className="ml-2"
                                                                onClick={() => {
                                                                    this.props.setPengembalian(item);
                                                                }}>
                                                                Pengembalian
                                                            </CButton>
                                                            :
                                                            <CButton color={"success"} size={"sm"} className="ml-2"
                                                                onClick={() => {
                                                                    this.props.setPengembalian(item, true);
                                                                }}>
                                                                View Pengembalian
                                                            </CButton>
                                                    }
                                                </td>
                                            ),
                                        'Status':
                                            (item) => (
                                                <td align="center">
                                                    <CBadge color={getBadge(item.Status)}>
                                                        {item.Status}
                                                    </CBadge>
                                                </td>
                                            ),
                                        'TanggalPinjam':
                                            (item) => (
                                                <td>
                                                    <Moment format="DD MMM YYYY">{item.TanggalPinjam}</Moment>
                                                </td>
                                            ),
                                        'TanggalKembali':
                                            (item) => (
                                                <td>
                                                    <Moment format="DD MMM YYYY">{item.TanggalKembali}</Moment>
                                                </td>
                                            ),
                                        'TanggalPengembalian':
                                            (item) => (
                                                <td>
                                                    {
                                                        (item.TanggalPengembalian && item.TanggalPengembalian != "0001-01-01") ?
                                                            <Moment format="DD MMM YYYY">{item.TanggalPengembalian}</Moment>
                                                            :
                                                            null
                                                    }
                                                </td>
                                            )
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
    data: state.peminjaman.data,
    isLoading: state.peminjaman.isLoading
});

export default connect(
    mapStateToProps,
    {
        getAll,
        add,
        edit,
        remove,
        setPengembalian
    }
)(ListPeminjaman);
