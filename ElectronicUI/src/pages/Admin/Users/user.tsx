/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

import { initializeIcons } from '@fluentui/react/lib/Icons';
import { DetailsList, SelectionMode } from '@fluentui/react/lib/DetailsList';
import { IColumn } from '@fluentui/react/lib/DetailsList';
initializeIcons();

const data = [
    { key: '1', name: 'John Doe', age: 30, occupation: 'Engineer' },
    { key: '2', name: 'Jane Smith', age: 25, occupation: 'Designer' },
];

const columns: IColumn[] = [
    { key: 'column1', name: 'Name', fieldName: 'name', minWidth: 100 },
    { key: 'column2', name: 'Age', fieldName: 'age', minWidth: 100 },
    { key: 'column3', name: 'Occupation', fieldName: 'occupation', minWidth: 100 },
];
function User() {
    return (
        <div style={{ display: 'flex' }}>
            <h1>User Management</h1> <br></br>
            <DetailsList
                items={data}
                columns={columns}
                selectionMode={SelectionMode.none}
            />
        </div>
    );
};

export default User;