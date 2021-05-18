import React, { Component } from 'react';
import {
    CButton,
    CCard,
    CCardBody,
    CCardHeader,
    CCol,
    CDataTable,
    CSelect,
    CRow
} from '@coreui/react';
import CIcon from '@coreui/icons-react';

const fields = ['JudulBuku', 'action'];

class ListPeminjamanBuku extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: props.data,
            listBuku: props.listBuku,
            peminjamanId: props.peminjamanId,
            canDelete: props.canDelete,
            bukuId: 0,
            judulBuku: null
        };

        this.onClickAdd = this.onClickAdd.bind(this);
        this.onClickDelete = this.onClickDelete.bind(this);
    }

    UNSAFE_componentWillReceiveProps(nextProps) {
        this.setState({
            peminjamanId: nextProps.peminjamanId,
            canDelete: nextProps.canDelete,
            listBuku: nextProps.listBuku,
            data: (nextProps.data != null) ? nextProps.data : null
        });
    }

    onClickAdd() {
        let detail = {
            PeminjamanId: this.state.peminjamanId,
            BukuId: this.state.bukuId,
            JudulBuku: this.state.judulBuku
        };

        this.props.onAdd(detail);

        this.setState({
            bukuId: 0,
            judulBuku: null
        });
    }

    onClickDelete(id) {
        this.props.onDelete(id);
    }

    render() {
        console.log("this.state", this.state);
        return (
            <>
                <CRow>
                    <CCol xs="12" lg="12">
                        <CCard>
                            <CCardHeader>
                                <CRow>
                                    <CCol xs="12" lg="6">Daftar Buku</CCol>
                                    {
                                        (this.state.canDelete) ?
                                            <CCol xs="12" lg="4">
                                                <CSelect custom
                                                    name="select" id="select" value={this.state.bukuId}
                                                    onChange={(e) => {
                                                        this.setState({
                                                            bukuId: parseInt(e.target.value),
                                                            judulBuku: e.target.options[e.target.selectedIndex].text
                                                        })
                                                    }}>
                                                    <option value="0">Please select buku</option>
                                                    {
                                                        (this.state.listBuku && this.state.listBuku.length > 0) ?
                                                            this.state.listBuku.map((item, index) => {
                                                                return (<option key={index} value={item.Id}>{item.Judul}</option>)
                                                            }) : null
                                                    }
                                                </CSelect>
                                            </CCol> : null
                                    }
                                    {
                                        (this.state.canDelete) ?
                                            <CCol xs="12" lg="2">
                                                <CButton block color="success" onClick={this.onClickAdd} className="pull-right">Add</CButton>
                                            </CCol> : null
                                    }
                                </CRow>
                            </CCardHeader>
                            <CCardBody>
                                <CDataTable
                                    items={this.state.data || []}
                                    fields={fields}
                                    itemsPerPage={5}
                                    pagination
                                    hover
                                    sorter
                                    scopedSlots={{
                                        'action':
                                            (item) => (
                                                <td align="center">
                                                    {
                                                        (this.state.canDelete) ?
                                                            <CButton color={"danger"} size={"sm"}>
                                                                <CIcon name="cil-trash" title="edit"
                                                                    onClick={() => {
                                                                        this.onClickDelete(item.BukuId);
                                                                    }} />
                                                            </CButton> : null
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

export default ListPeminjamanBuku;
